using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ControllsFatigueInfluence : MonoBehaviour
{
    public int fatigueValue = 50;
    public FirstPersonController firstpersoncontroller;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // get fatigue value
        
        // get overdose value
        
        // adjust movespeed
        MovementSpeedAdjust();
        givePerlinNoise();
    
    }

    //movement speed gets adjusted when fatigue value changes
    public void MovementSpeedAdjust() 
    {
        firstpersoncontroller.MoveSpeed = fatigueValue*0.1f;        
    }

    public int xCoord = 100;
    public int yCoord = 100;

    public void givePerlinNoise() 
    {
         

         for (int xCoord = 0; xCoord <= 100; xCoord++)
         {
            for (int yCoord = 0; yCoord <= 100; yCoord++)
            {
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                print(sample);
            }   
         }
    }
    
    
}
