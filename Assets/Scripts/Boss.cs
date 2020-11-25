using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject slash;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerBehavior player;
    private int health = 5;
    private GameManager gm;
    public float jumpSpeed = 5f;


    private bool jumpQueued = false;
    private bool attackQueued = false;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumpQueued)
        {
            jumpQueued = true;
            Vector2 newVelocity = rb.velocity;
            newVelocity.y = jumpSpeed;

            rb.velocity = newVelocity;
            StartCoroutine(jump());

        }

        if (!attackQueued)
        {
            attackQueued = true;
            StartCoroutine(attack());
        }

        if(player.transform.position.x * 1.5 > gameObject.transform.position.x)
        {
            rb.velocity = new Vector2(3f,0);
        }else if (player.transform.position.x * 1.3 < gameObject.transform.position.x)
        {
            rb.velocity = new Vector2(-3f, 0);
        }else
        {
            rb.velocity = new Vector2(0, 0);

        }
    }


    private IEnumerator attack()
    {
        //create instance of attack 


        animator.SetTrigger("slash");
        float val = spriteRenderer.flipX == true
            ? transform.position.x - 1
            : transform.position.x + 1;

        GameObject slashInstance = Instantiate(slash, new Vector3(val, transform.position.y, 0), Quaternion.identity);
        SlashController slashcon = slashInstance.GetComponent<SlashController>();

        float speed = spriteRenderer.flipX == true
            ? -13f - 2f
            : 13f + 2f;
        slashcon.setDirection(spriteRenderer.flipX);
        slashcon.setSpeed(speed);

        yield return new WaitForSecondsRealtime(Random.Range(2,4));
        attackQueued = false;
    }

    private IEnumerator jump()
    {

        yield return new WaitForSecondsRealtime(Random.Range(4, 6));
        jumpQueued = false;
    }

    public void removeHealth()
    {
        if (health <= 0)
        {
            gm.toggleWalls();

            Destroy(gameObject);
            gm.showMessage();
        }
        health -= 1;
    }
}
