using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOrientation : MonoBehaviour
{
    Transform cameraTransform;
    Transform alarmStatusCanvas;

    // Start is called before the first frame update
    void Start()
    {
        this.cameraTransform = GameObject.FindWithTag("MainCamera").transform; ;
        alarmStatusCanvas = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(alarmStatusCanvas.transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
    }
}
