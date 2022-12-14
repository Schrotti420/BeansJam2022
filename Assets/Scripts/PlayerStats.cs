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

    //Attention variables
    private float attention = 0;
    float timerAtt = 0;
    bool startTimer = false;
    float timeAtt = 0;
    public float Attention { get { return attention; } }


    // Time in seconds for Fatigue to decrease by one
    [SerializeField]
    private float fatigueRate = 2.0f;
    private float oneSecondTimer = 0.0f;

    //Variables for Caught by Guard
    bool caughtByGuard = false;
    public bool CaughtByGuardStatus { get { return caughtByGuard; } }


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
        Debug.Log("Attention  " + attention.ToString());
        if (gameOver)
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

        if(startTimer)
        {
            timerAtt += Time.deltaTime;
            if (timerAtt < timeAtt)
            {
                attention = timerAtt/timeAtt;
                
            }
        }
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

    public void attentionStatus(float value, bool startTimer, float time)
    {
        attention = value;
        startTimer = true;
        timeAtt = time;

    }

    public void CaughtByGuard(bool value)
    {
        caughtByGuard = value;

    }

    private void Asleep()
    {
        if(gameOver)
            return;

        gameOver = true;
        thirdPersonSwitch.FallAsleep();
    }
    private void Overdose()
    {
        if(gameOver)
            return;

        gameOver = true;
        thirdPersonSwitch.Overdose();
    }

    public void Caught()
    {
        if (gameOver)
            return;

        gameOver = true;
        TimeManager.Instance.ShowCaughtScreen();
    }
}
