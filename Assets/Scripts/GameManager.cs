using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private Boolean pause;
    private Scripts scripts;
    private Text text;
    public Canvas canvas;

    private float timer;
    private float timePerChar;
    private string textToWrite;
    private int characterIndex;
    private bool canwin;
    private PlayerBehavior player;

    // Start is called before the first frame update
    void Start()
    {
        scripts = gameObject.GetComponent<Scripts>();
        textToWrite = "HELLO MY BABY HELLO MY HONEY HELLO MY RAGTIME GAAAAAAAAAAAAAAL";
        characterIndex = 0;
        text =  null;
        timePerChar = 0.060f;
        canwin = false;
        pause = false;
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

        if (text != null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                //Display the next character
                timer += timePerChar;
                characterIndex++;
                text.text = textToWrite.Substring(0, characterIndex);
                

                if (characterIndex >= textToWrite.Length)
                {
                    text = null;
                    canwin = true;
                    return;
                }
            }
        }


    }

    public void winlevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            //SET CANVAS TEXT HERE 
            SceneManager.LoadScene(activeScene.buildIndex + 1);
        }
    }

    public void showMessage()
    {
        canvas.gameObject.SetActive(true);
        text = canvas.GetComponent<UIHelper>().getText();

        player.Pause();
    }

    public void retryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  

}
