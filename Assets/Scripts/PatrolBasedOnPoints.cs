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
    Image mask;
    Color currentColor;

    //Image[] images;

    public Sprite spriteInitialState;
    public Sprite spriteAlarmState1;
    public Sprite spriteAlarmState2;

    int alarmLevel = 0;

    float MinDist = 5;
    float MoveSpeed = 3;

    Slider slider;
    CanvasGroup canvasGroup;
    float targetProgress = 7f;
    public float SliderSpeed = 0.05f;

    float startTime = 0;
    bool startTimer = false;
    public float targetTime = 15f;

    DrugAbuse drugAbuse;

    Image Fill;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        this.player = GameObject.FindWithTag("Player").transform;
        alarmStatusImage = agent.GetComponentInChildren<Image>();


        /*images = agent.GetComponentsInChildren<Image>();
        for(int i=0; i<images.Length; i++)
        {
            if (images[i].name == "AlarmStatusImage")
                alarmStatusImage = images[i];
            else
                mask = images[i];
        }*/
        slider = agent.GetComponentInChildren<Slider>();
        canvasGroup = slider.GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0;
        Fill = slider.GetComponentInChildren<Image>();

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
        Debug.Log(destPoint.ToString());

        CheckCulprit();
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

        bool isDrugPicked = player.GetComponent<DrugAbuse>().isDrugPicked();
        //Debug.Log("timer " + startTime.ToString());

        if (angleToPlayer <= 50 &&
           distanceFromPlayer <= 10 &&
           isDrugPicked)
        {
            if (alarmLevel == 0)
            {
                Debug.Log("Alert Level 1");

                //Switch Sprite
                alarmStatusImage.sprite = spriteAlarmState1;

                alarmLevel = 1;

                startTimer = true;
                canvasGroup.alpha = 1;
            }
            else if (alarmLevel == 1)
            {
                //canvasGroup.alpha = 0;

                //Switch Sprite
                alarmStatusImage.sprite = spriteAlarmState2;
                alarmLevel = 2;
                Fill.color = Color.red;

                Debug.Log("Alert Level 2");
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

           //CheckCulprit();
        }
        else if (alarmLevel == 1)
        {
            if(startTimer)
                startTime += Time.deltaTime;

            ChaseCulprit();


            //Debug.Log(startTime.ToString());

            Debug.Log("timer " + startTime.ToString() + " " + targetTime.ToString() + " " + targetProgress.ToString());
            if (startTime <= targetTime && startTime > 1.5)
            {
                CheckCulprit();
                if (slider.value < targetProgress)
                    //slider.value += (targetProgress/ (targetTime-startTime))* Time.deltaTime*SliderSpeed;
                    slider.value = (startTime/(targetTime-1.5f))*targetProgress;
            }
            else if(startTime > targetTime)
            {

                startTime = 0;
                startTimer = false;
                alarmLevel = 0;
                slider.value = 0;

                //Switch Sprite
                alarmStatusImage.sprite = spriteInitialState;
                Debug.Log("Timer out");
                
            }
        }
    }
}