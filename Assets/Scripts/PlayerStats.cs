using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    public static PlayerStats Instance { get { return _instance; } }

    public ThirdPersonSwitch thirdPersonSwitch;

    // Fatigue/overdose min = 0 -> fatigue, max = 100 -> overdose
    private int fatigue = 50;
    public int Fatigue { get { return fatigue; } }
    
    // Time in seconds for Fatigue to decrease by one
    [SerializeField]
    private float fatigueRate = 2.0f;
    private float oneSecondTimer = 0.0f;

    public bool gameOver = false;
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

        if(!thirdPersonSwitch)
            thirdPersonSwitch = FindObjectOfType<ThirdPersonSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
            return;

        oneSecondTimer += Time.deltaTime;
        if(oneSecondTimer > fatigueRate)
        {
            fatigue -= 1;
            oneSecondTimer -= fatigueRate;

            //Debug.Log(fatigue);

            if (fatigue <= 0)
                Asleep();
        }
        
        // if(Input.anyKeyDown)
        //     PlayerStats.Instance.IncreaseFatigue(5);
    }

    public void IncreaseFatigue(int value)
    {
        fatigue = fatigue - value <= 0 ? 0 : fatigue - value;

        if (fatigue <= 0)
            Asleep();
    }

    public void IncreaseOverdose(int value)
    {
        fatigue = fatigue + value >= 100 ? 100 : fatigue + value;

        if(fatigue >= 100)
            Overdose();
    }

    private void Asleep()
    {
        gameOver = true;
        thirdPersonSwitch.FallAsleep();
    }
    private void Overdose()
    {
        gameOver = true;
        thirdPersonSwitch.Overdose();
    }
}
