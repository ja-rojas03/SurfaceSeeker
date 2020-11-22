using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum PowerUpEnum
{
    Snake,
}
public class PowerUp : MonoBehaviour
{




    public PowerUpEnum powerUp;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            switch (powerUp)
            {
                case PowerUpEnum.Snake:
                    snakePowerUp snakePowerUp = collision.gameObject.GetComponent<snakePowerUp>();
                    if (snakePowerUp != null)
                    {
                        snakePowerUp.Activate();
                    }
                    break;

                default:
                    break;

            }
            Destroy(this.gameObject);
        }
        // Try to add power:
        
        
        
    }
}
