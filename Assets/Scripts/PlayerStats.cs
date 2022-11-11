using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    public static PlayerStats Instance { get { return _instance; } }

    // Fatigue/overdose min = 0 -> fatigue, max = 100 -> overdose
    private int fatigue = 50;
    public int Fatigue { get { return fatigue; } }
    
    // Time in seconds for Fatigue to decrease by one
    private float fatigueRate = 2.0f;
    private float oneSecondTimer = 0.0f;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fatigue = 50;
    }

    // Update is called once per frame
    void Update()
    {
        oneSecondTimer += Time.deltaTime;
        if(oneSecondTimer > fatigueRate)
        {
            fatigue -= 1;
            oneSecondTimer -= fatigueRate;

            Debug.Log(fatigue);
        }
        
        if(Input.anyKeyDown)
            PlayerStats.Instance.IncreaseFatigue(5);
    }

    public void IncreaseFatigue(int value)
    {
        fatigue = fatigue - value <= 0 ? 0 : fatigue - value;
    }

    public void IncreaseOverdose(int value)
    {
        fatigue = fatigue + value >= 100 ? 100 : fatigue + value;
    }

    private void Asleep()
    {
        //TODO
    }
    private void Overdose()
    {
        //TODO
    }
}
