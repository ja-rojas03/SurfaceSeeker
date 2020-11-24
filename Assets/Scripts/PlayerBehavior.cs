using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;

    public float speed = 3f;
    public float jumpSpeed = 6f;
    public float runSpeed = 6f;
    public bool isRunning = false;
    public float availableJumps = 2f;

    private SkillController skillController;

    public Transform RightWallDetector;
    public Transform LeftWallDetector;

    public Vector3 WallDetectorSize;
    public bool canWallJump = false;
    public bool isWallJumping = false;
    public bool isJumpingFromRight = false;
    public bool isJumpingFromLeft = false;
    private bool canClimb = false;
    private bool pause;
    private float initialGravity;
    private Vector3 initialPosition;
    private Vector3 checkpoint;

    public PhysicsMaterial2D WallJumpSlideMaterial;
    private SpriteRenderer spriteRenderer;

    private snakePowerUp snakePowerUp;

    private int health = 3;

   
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        skillController = gameObject.GetComponent<SkillController>();
        snakePowerUp = gameObject.GetComponent<snakePowerUp>();
        initialPosition = transform.position;
        initialGravity = rb.gravityScale;
        checkpoint = initialPosition;


        pause = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (pause) return;

        Vector2 newVelocity = rb.velocity;
        bool isSnake = snakePowerUp.isActive;


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (spriteRenderer != null)
            {
                // flip the sprite
                spriteRenderer.flipX = true;
            }
            newVelocity.x = -speed;
            animator.SetBool("walk", true);
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
            newVelocity.x = speed;
            animator.SetBool("walk", true);
            
        }
        else
        {
            newVelocity.x = 0;
            animator.SetBool("walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isSnake && availableJumps > 0)
        {
            
            newVelocity.y = jumpSpeed;
            removeJump();
            
        }

        if(Input.GetKey(KeyCode.UpArrow) && canClimb)
        {
            newVelocity.y = jumpSpeed;
            rb.gravityScale = 0;

            animator.SetBool("isClimbing", true);
        }

        if (Input.GetKey(KeyCode.DownArrow) && canClimb)
        {
            newVelocity.y = -jumpSpeed;
            rb.gravityScale = 0;


            animator.SetBool("isClimbing", true);
        }






        rb.velocity = newVelocity;
        

        if(skillController.hasSkill(Skills.DASH) && Input.GetKey(KeyCode.LeftShift))
        {
            skillController.dash();
            isRunning = true;
            animator.SetTrigger("run");
        } else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 3f;
        }
        // Check left side colliding:
         Collider2D[] collidersLeft =  Physics2D.OverlapBoxAll(LeftWallDetector.position, WallDetectorSize, 0f, LayerMask.GetMask("JumpableWall"));
        
        //Check right side colliding:
        Collider2D[] collidersRight = Physics2D.OverlapBoxAll(RightWallDetector.position, WallDetectorSize, 0f, LayerMask.GetMask("JumpableWall"));

        //Set can wall jump:
        if( (collidersLeft.Length > 0 || collidersRight.Length > 0) && (skillController.hasSkill(Skills.WALLJUMP) && Input.GetKey(KeyCode.Space)) )
        {

            canWallJump = true;
        } else
        {
            canWallJump = false;
        }


        if ((collidersLeft.Length > 0 || collidersRight.Length > 0) && (skillController.hasSkill(Skills.WALLJUMP)))
        {
            
           canWallJump = true;
           
        }
        else
        {
            animator.SetBool("wallJump", false);
            canWallJump = false;
        }

        // Animate + Flip on x:
        if(collidersLeft.Length > 0 && (skillController.hasSkill(Skills.WALLJUMP)))
        {
            animator.SetBool("wallJump", true);
            animator.SetBool("walk", false);
            spriteRenderer.flipX = false;
            

        }

        if (collidersRight.Length > 0 && (skillController.hasSkill(Skills.WALLJUMP)))
        {
            animator.SetBool("wallJump", true);
            animator.SetBool("walk", false);
            spriteRenderer.flipX = true;

        }



        // Activate Friction:
        if (collidersLeft.Length > 0 || collidersRight.Length > 0)
        {
            rb.sharedMaterial = WallJumpSlideMaterial;
        }else
        {
            rb.sharedMaterial = null;
        }

        

        //Activate wall jump:
        if(!isWallJumping && canWallJump && Input.GetKeyDown(KeyCode.Space))
        {
            //Go ahead and jump
            isWallJumping = true;
            isJumpingFromRight = collidersRight.Length > 0;
            isJumpingFromLeft = collidersLeft.Length > 0;
            Invoke("StopWallJump", 0.15f);
        }

        


        if(isWallJumping)
        {
            //animator.SetBool("wallJump", true);
            if (isJumpingFromRight)
            {
                
                rb.velocity = new Vector2(-15, 10);
            }else
            {
                
                rb.velocity = new Vector2(15, 10);
            }

        }

        animator.SetBool("isSnake", isSnake);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            canClimb = true;
        }

        if (collision.CompareTag("Chest"))
        {
            ChestController chest = collision.gameObject.GetComponent<ChestController>();
            if(!chest.isOpen)
            {

                chest.isOpen = true;
                if(!skillController.hasSkill(chest.skill))
                {
                    skillController.obtainSkill(chest.skill);
                }
                Debug.Log(skillController.availableSkills);
                    
            }
            
        }

        if (collision.CompareTag("Checkpoint"))
        {
            checkpoint = collision.gameObject.transform.position;
        }





    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected");


        if (collision.gameObject.tag == "KillingGround")
        {
            if(checkpoint != initialPosition)
            {

            }else
            {
            transform.position = initialPosition;
            }

            transform.position = checkpoint != initialPosition 
                ? checkpoint 
                : initialPosition;
            //TAKE LIFE POINT AWAY

            removeHealth();

        }


        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Health" + health);
            if (health > 0)
            {

                removeHealth();
            Debug.Log("Health AFTER DMG" + health);
            }
            else
            {
                //Do death thingy here
                // maybe reset game ? DO NOT DESTROY OBJ
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>().retryLevel();
                Debug.Log("DED");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            canClimb = false;
            Debug.Log("EXITED LADDER");
            animator.SetBool("isClimbing", false);
            rb.gravityScale = initialGravity;


        }
    }


    private void OnDrawGizmosSelected()
    {
        // Draw left "collider"
        Gizmos.color = Color.green;
        Gizmos.DrawCube(LeftWallDetector.position, WallDetectorSize);
        // Draw Right "collider"
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(RightWallDetector.position, WallDetectorSize);
    }

    public void StopWallJump()
    {
        isWallJumping = false;
    }

    public void Pause()
    {
        this.pause = !pause;
        rb.velocity = new Vector3(0, 0, 0);
        animator.SetBool("walk", false);

    }

    public void resetJump()
    {
        availableJumps = 2f;
    }

    public void removeJump()
    {
        if(availableJumps > 0)
        {
            availableJumps -= 1f;
        }
    }

    public void removeHealth()
    {
        health -= 1;
    }


}
