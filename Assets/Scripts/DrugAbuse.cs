using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugAbuse : MonoBehaviour
{
    bool drugPicked = false;
    float timer = 0f;
    bool startTimer = false;

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
        }
        else if (collision.gameObject.tag == "Water")
        {
            Debug.Log("Water Picked Up");
            PlayerStats.Instance.IncreaseFatigue(5);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Beer")
        {
            Debug.Log("Beer Picked Up");
            PlayerStats.Instance.IncreaseFatigue(2);
            Destroy(collision.gameObject);
        }
    }

    public bool isDrugPicked()
    {

        return drugPicked;
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer)
            timer += Time.deltaTime;

        if(timer > 1.5)
        {
            drugPicked = false;
            timer = 0;
            startTimer = false;
        }

    }
}
