using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    // Start is called before the first frame update

    public Skills skill;

    public bool isOpen = false;
    private Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isOpen)
        {
            
            Debug.Log("Player is in chest");
            animator.SetTrigger("openChest");


        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isOpen)
        {
            Debug.Log("Player left chest");
            
        }
    }

    
}
