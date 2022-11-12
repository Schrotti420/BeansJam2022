using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLocalisation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "StageZone")
        {
            Debug.Log($"Entering StageZone {other.gameObject.name}");
            SoundLocationManager.Instance.SwitchStage(other.gameObject.name);
        }
    }
}
