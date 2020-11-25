using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject walls;
    public GameObject camera; 
    public GameObject boss;
    private GameManager gm;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Slash"))
        {
            //walls.SetActive(true);
            gm.toggleWalls();
            camera.GetComponent<Camera>().orthographicSize = 10f;
            //Instantiate finall boss
            GameObject bossInstance = Instantiate(boss , new Vector3(51.27f, 66.5f, 0), Quaternion.identity);
            Boss bossCon= bossInstance.GetComponent<Boss>();
            bossCon.GetComponent<SpriteRenderer>().flipX = true;
            Destroy(this);
        }
    }
}
