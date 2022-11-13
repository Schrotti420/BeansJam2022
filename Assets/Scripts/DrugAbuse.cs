using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugAbuse : MonoBehaviour
{

    private static DrugAbuse _instance;
    public static DrugAbuse Instance { get { return _instance; } }

    bool drugPicked = false;
    float timer = 0f;
    float timerWater = 0f;
    float timerBeer = 0f;
    bool startTimer = false;
    bool startTimerWater = false;
    bool startTimerBeer = false;
    bool waterPicked = false;
    bool beerPicked = false;

    float attention = 0;

    public float timeAtt = 15;

    public float Attention { get { return attention; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Drug")
        {
            Debug.Log("Drug Picked Up");
            PlayerStats.Instance.IncreaseOverdose(10);
            Destroy(collision.gameObject);
            drugPicked = true;
            startTimer = true;
            attention = 2;
            if(PlayerStats.Instance.CaughtByGuardStatus) 
                PlayerStats.Instance.attentionStatus(2f,true,timeAtt);
        }
        else if (collision.gameObject.tag == "Water")
        {
            Debug.Log("Water Picked Up");
            PlayerStats.Instance.IncreaseFatigue(5);
            Destroy(collision.gameObject);
            waterPicked = true;
            attention = 2;
            PlayerStats.Instance.attentionStatus(2f, true, timeAtt);

        }
        else if (collision.gameObject.tag == "Beer")
        {
            Debug.Log("Beer Picked Up");
            PlayerStats.Instance.IncreaseFatigue(2);
            Destroy(collision.gameObject);
            beerPicked = true;
            attention = 0;
            PlayerStats.Instance.attentionStatus(1f,false, timeAtt);
        }
    }

    public bool isDrugPicked()
    {
        return drugPicked;
    }

    public bool isWaterPicked()
    {
        return waterPicked;
    }

    public bool isBeerPicked()
    {
        return beerPicked;
    }

    // Update is called once per frame
    void Update()
    {
        //Drug Timer
        if(startTimer)
            timer += Time.deltaTime;

        if(timer > 1.5)
        {
            drugPicked = false;
            timer = 0;
            startTimer = false;
        }

        //Water Timer
        if (startTimerWater)
            timerWater += Time.deltaTime;

        if (timerWater > 1.5)
        {
            waterPicked = false;
            timerWater = 0;
            startTimerWater = false;
        }

        //Beer Timer
        if (startTimerBeer)
            timerBeer += Time.deltaTime;

        if (timerBeer > 1.5)
        {
            beerPicked = false;
            timerBeer = 0;
            startTimerBeer = false;
        }

    }
}
