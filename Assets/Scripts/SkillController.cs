using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    // Start is called before the first frame update
   public List<Skills> availableSkills;
    public PlayerBehavior player;
    public Skills WALLJUMP;


    void Start()
    {
        availableSkills = new List<Skills>();
        player = gameObject.GetComponent<PlayerBehavior>();

        
    }

    public bool hasSkill(Skills s)
    {
        return availableSkills.IndexOf(s) != -1;
    }

    public void obtainSkill(Skills s)
    {
         availableSkills.Add(s);
    }

    public void dash()
    {

        player.speed = player.runSpeed;
        
    }


}



