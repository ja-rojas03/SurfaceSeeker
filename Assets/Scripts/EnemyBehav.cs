using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehav : MonoBehaviour
{
    // Publics
    // The speed at which the enemy moves
    public float xSpeed = 1.5f;
    // The damage (in seconds, since we rely on time) done by the enemy
    public float damage = 5f;
    // The amount of knockback force to use when clashing with the player
    public float knockback = 100f;

    // Privates
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    //private AudioSource audioSource;
    //private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       // audioSource = GetComponent<AudioSource>();
       // boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = xSpeed < 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb != null)
        {

            Vector2 newVelocity = rb.velocity;
            newVelocity.x = xSpeed;
            rb.velocity = newVelocity;
        }
    }

    public void Kill()
    {
       // GameManager.GetGameManager().PlaySound("Hit");
        gameObject.SetActive(false);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        // Since the collision belongs to the main component, we need to access the game object of the collider
        // this way, we can identify the StompCheck object via its tag.
        GameObject colObject = col.gameObject;
        string tag = colObject.tag;
        // If we hit a wall or an enemy, invert the speed
        if (colObject.gameObject.CompareTag("KillingGround") || colObject.gameObject.CompareTag("Wall") || colObject.gameObject.CompareTag("Enemy"))
        {
            xSpeed = -xSpeed;
        }
        // If we hit the player, damage them only if we're still axctive
        else if (colObject.CompareTag("Player") && gameObject.activeSelf)
        {
           //PlayerBehavior playerHealth = colObject.GetComponent<PlayerBehavior>();
            //playerHealth.remo
           // CharacterController2D playerController = colObject.GetComponent<CharacterController2D>();
            //playerHealth.remainingLifeTime -= damage;
            // Knock the player back so that they don't keep colliding, and run the other way
            //StartCoroutine(playerController.PerformKnockback(new Vector2(xSpeed > 0 ? knockback : -knockback, knockback)));
            xSpeed = -xSpeed;

        }
    }
}
