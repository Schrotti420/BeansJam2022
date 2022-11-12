// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;


public class PatrolBasedOnPoints : MonoBehaviour
{
    //Patrol variables
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    //Alert Status variables
    Transform player;
    Image alarmStatusImage;
    Color currentColor;

    public Sprite spriteInitialState;
    public Sprite spriteAlarmState1;
    public Sprite spriteAlarmState2;

    int alarmLevel = 0;

    float MinDist = 1;
    float MoveSpeed = 3;

    Slider slider;
    float targetProgress = 100;
    public float SliderSpeed = 0.5f;

    float startTime = 0;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        this.player = GameObject.FindWithTag("Player").transform;
        alarmStatusImage = GameObject.Find("AlarmStatusImage").GetComponent<Image>();
        slider = GameObject.Find("AlarmStatusSlider").GetComponent<Slider>();

        

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        NormalPatrol();
    }


    void NormalPatrol()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        //destPoint = (destPoint + 1) % points.Length;
        destPoint = Random.Range(0,points.Length);
        Debug.Log("NormalPatrol");
    }

    void ChaseCulprit()
    {

        transform.LookAt(player);

        if (Vector3.Distance(this.transform.position, player.position) >= MinDist)
        {
            Vector3 direction = Vector3.Normalize(player.position - this.transform.position);

            agent.destination += direction * MoveSpeed * Time.deltaTime;

            //Debug.Log("Move");

            if (Vector3.Distance(this.transform.position, player.position) < MinDist)
            {
                
                
            }
        }
    }

    void Behaviour(int AlarmState)
    {
        switch (AlarmState)
        {
            case 0: 
                NormalPatrol();
                break;
            case 1:
                ChaseCulprit();
                break;
        }
    }

    void IncrementSlider(float newProgress)
    {
        slider.value += newProgress;
    }

    void CheckCulprit()
    {
        Vector3 direction = player.position - this.transform.position;

        float distanceFromPlayer = Vector3.Distance(player.position, this.transform.position);
        float angleToPlayer = Vector3.Angle(direction, this.transform.forward);

        //Debug.Log(distanceFromPlayer.ToString());
        //Debug.Log(angleToPlayer.ToString());

        if (angleToPlayer <= 30 &&
           distanceFromPlayer <= 10 /*&& erwischt*/ )
        {
            if (alarmLevel == 0)
            {
                Debug.Log("Alert Level 1");

                //Switch Sprite
                alarmStatusImage.sprite = spriteAlarmState1;
                alarmLevel = 1;
                startTime = Time.time;
            }
            else if (alarmLevel == 1)
            {
                Debug.Log("Alert Level 2");

                //Switch Sprite
                alarmStatusImage.sprite = spriteAlarmState2;
                alarmLevel = 2;
            }

        }
    }



    void Update()
    {

        if (alarmLevel == 0)
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            if ((!agent.pathPending && agent.remainingDistance < 0.5f))
                NormalPatrol();

            CheckCulprit();
        }
        else if (alarmLevel == 1)
        {
            ChaseCulprit();
            float currentTime = Time.time;

            if (currentTime - startTime <= 1000)
            {
                CheckCulprit();
            }
            else
            {
                if (slider.value < targetProgress)
                    slider.value += SliderSpeed * Time.deltaTime;

                alarmLevel = 0;
            }
        }
    }
}