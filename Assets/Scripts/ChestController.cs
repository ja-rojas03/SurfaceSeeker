using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    // Start is called before the first frame update

    public Skills skill;

    public bool isOpen = false;

    public bool isWinnable = false;

    private Animator animator;
    private GameManager gm;


    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && isWinnable)
        {

            //DO SOMETHING LIKE : 
            //GameManager.winCourse(historyText); 
            gm.showMessage();


        }


        if (collision.CompareTag("Player") && !isOpen && !isWinnable)
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
