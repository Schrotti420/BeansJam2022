using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertStatus : MonoBehaviour
{
    Transform player;
    Image alarmStatusImage;
    Color currentColor;

    public Sprite spriteInitialState;
    public Sprite spriteAlarmState1;



    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player").transform;
        alarmStatusImage = GameObject.Find("AlarmStatusImage").GetComponent<Image>();


    }

    // Update is called once per frame
    void Update()
    {
        

        Vector3 direction = player.position - this.transform.position;

        float distanceFromPlayer = Vector3.Distance(player.position, this.transform.position);
        float angleToPlayer = Vector3.Angle(direction, this.transform.forward);

        //Debug.Log(distanceFromPlayer.ToString());
        //Debug.Log(angleToPlayer.ToString());

        if (angleToPlayer<=30 &&
           distanceFromPlayer<=10)
        {
            Debug.Log("Alert Level 1");

            //Switch Sprite
            alarmStatusImage.sprite = spriteAlarmState1;

        }
    }
}
