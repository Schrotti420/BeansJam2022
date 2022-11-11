using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ControllsFatigueInfluence : MonoBehaviour
{
    public int fatigueValue = 5;
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
    
    }

    public void MovementSpeedAdjust() 
    {
        if (fatigueValue <= 100) 
        {
            firstpersoncontroller.MoveSpeed = 10.0f;
            
        }
        if (fatigueValue <= 90) 
        {
            firstpersoncontroller.MoveSpeed = 9.0f;
            
        }
        if (fatigueValue <= 80) 
        {
            firstpersoncontroller.MoveSpeed = 8.0f;
            
        }
        if (fatigueValue <= 70) 
        {
            firstpersoncontroller.MoveSpeed = 7.0f;
            
        }
        if (fatigueValue <= 60) 
        {
            firstpersoncontroller.MoveSpeed = 6.0f;
            
        }
        if (fatigueValue <= 50) 
        {
            firstpersoncontroller.MoveSpeed = 5.0f;
            
        }
        if (fatigueValue <= 40) 
        {
            firstpersoncontroller.MoveSpeed = 4.0f;
            
        }
        if (fatigueValue <= 30) 
        {
            firstpersoncontroller.MoveSpeed = 3.0f;
            
        }
        if (fatigueValue <= 20) 
        {
            firstpersoncontroller.MoveSpeed = 2.0f;
            
        }
        if (fatigueValue <= 10) 
        {
            firstpersoncontroller.MoveSpeed = 1.0f;
            
        }
        
        
        
        
    }
}
