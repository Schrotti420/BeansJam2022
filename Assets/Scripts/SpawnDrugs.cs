using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDrugs : MonoBehaviour
{

    public float interval = 5;
    public GameObject spawnObject;
    public Vector3 minPosition_firstFloor;
    public Vector3 maxPosition_firstFloor;
    public Vector3 minPosition_secFloor;
    public Vector3 maxPosition_secFloor;
    float timer;

    public int spawnCountInOneFrame_FirstFloor = 3;

    private GameObject[] getCount;
    public int maxAmountDrugs_secFloor = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        getCount = GameObject.FindGameObjectsWithTag("Drug");
        int count = getCount.Length;
        int countFirstFloor = 0;
        int countSecFloor = 0;
        for (int j=0; j<count; j++)
        {
            Vector3 pos = getCount[j].transform.position;
            if (pos.y < 5)
            {
                countFirstFloor += 1;
            }
            else countSecFloor += 1;
        }

        //Debug.Log("Drug Number 1st Floor" + countFirstFloor);
        //Debug.Log("Drug Number 2st Floor" + countSecFloor);

        timer += Time.deltaTime;
        if (timer >= interval)
        {


            Quaternion initialQuaternion = transform.rotation;
            if (countFirstFloor < maxAmountDrugs_secFloor * spawnCountInOneFrame_FirstFloor)
            {
                for (int i = 0; i < spawnCountInOneFrame_FirstFloor; i++)
                {
                    Quaternion randomQuaternionfF = Quaternion.Euler(initialQuaternion.x, Random.Range(0f, 360f), initialQuaternion.z);

                    Vector3 randomPosition_firstFloor = new Vector3(
                        Random.Range(minPosition_firstFloor.x, maxPosition_firstFloor.x),
                        Random.Range(minPosition_firstFloor.y, maxPosition_firstFloor.y),
                        Random.Range(minPosition_firstFloor.z, maxPosition_firstFloor.z)
                    );
                    Instantiate(spawnObject, randomPosition_firstFloor, randomQuaternionfF);

                }
            }
            if (countSecFloor < maxAmountDrugs_secFloor)
            {
                Vector3 randomPosition_secFloor = new Vector3(
                    Random.Range(minPosition_secFloor.x, maxPosition_secFloor.x),
                    Random.Range(minPosition_secFloor.y, maxPosition_secFloor.y),
                    Random.Range(minPosition_secFloor.z, maxPosition_secFloor.z)
                );

                Quaternion randomQuaternion = Quaternion.Euler(initialQuaternion.x, Random.Range(0f, 360f), initialQuaternion.z);
                Instantiate(spawnObject, randomPosition_secFloor, randomQuaternion);
            }
            timer -= interval;
        }
    }
}
