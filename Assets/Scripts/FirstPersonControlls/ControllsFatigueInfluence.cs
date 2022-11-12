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
       CalcPerlinNoise();
    }

    // Update is called once per frame
    void Update()
    {
        // get fatigue value
        
        // get overdose value
        
        // adjust movespeed
        MovementSpeedAdjust();
        
    
    }

    //movement speed gets adjusted when fatigue value changes
    public void MovementSpeedAdjust() 
    {
        firstpersoncontroller.MoveSpeed = fatigueValue*0.1f;        
    }

    public float xCoord;
    public float yCoord = 0.0f;
    int arraycoord = 0;

    public void CalcPerlinNoise() 
    {
        float[] floatArray = new float[100];
             
         for (float xCoord = 1; xCoord <= 100; xCoord++)
         {
            float sample = Mathf.PerlinNoise(xCoord, yCoord);
            floatArray[arraycoord] = sample;
            arraycoord++;
         }
         foreach (float coord in floatArray)
         {
            print(coord);
         }
         
    }

    // public void drunkWalk()
    // {
    //     if(fatigueValue >= 70)
    //     {
    //         if (Input.getKey("up"))
    //         {
    //             calcDrunkWalk();
    //         }
            
    //         if (Input.getKey("down"))
    //         {
    //             calcDrunkWalk();
    //         }
    //     }
    // }

    public void calcDrunkWalk()
    {

    }
    
    
}
