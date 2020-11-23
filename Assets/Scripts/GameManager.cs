using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void winlevel(string text)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
        //SET CANVAS TEXT HERE 
            SceneManager.LoadScene(activeScene.buildIndex + 1);
        }
        else
        {
            Debug.Log("gg");
        }
    }
}
