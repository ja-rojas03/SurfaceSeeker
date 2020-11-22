using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakePowerUp : MonoBehaviour
{
    public bool isActive = false;
    public BoxCollider2D snakeCollider;
    public BoxCollider2D[] playerColliders;
    public float activatonTimeRemaining = 0f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        isActive = true;
        activatonTimeRemaining = 5f;
        foreach(BoxCollider2D collider in playerColliders)
        {
            collider.enabled = false;

        }

        snakeCollider.enabled = true;
        StartCoroutine(ActivationTimeCountDown());
    }

    public IEnumerator ActivationTimeCountDown()
    {
        while(activatonTimeRemaining > 0)
        {
            activatonTimeRemaining -= Time.deltaTime;
            yield return null;
        }

        foreach (BoxCollider2D collider in playerColliders)
        {
            collider.enabled = true;

        }
        snakeCollider.enabled = false;

        isActive = false;
        activatonTimeRemaining = 0f;
    }
}
