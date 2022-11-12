using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpotLightAnimator : MonoBehaviour
{
    [SerializeField]
    float degreesPerSecond;

    [SerializeField]
    Vector2 minMaxYRotation;
    [SerializeField]
    Transform yRotationRoot;
    [SerializeField]
    Vector2 minMaxZRotation;
    [SerializeField]
    Transform zRotationRoot;

    private bool yAxisPositive = true, zAxisPositive = true;

    // Update is called once per frame
    void Update()
    {
        PingPong();

        Debug.Log($"Y Direction: {yAxisPositive}");
        Debug.Log($"Z Direction: {zAxisPositive}");
    }
    private float AddRotation(bool isPositiveDirection, float rotation)
    {
        if (isPositiveDirection) return rotation + (degreesPerSecond * Time.deltaTime);
        else return rotation - (degreesPerSecond * Time.deltaTime);
    }
    public void PingPong()
    {
        if(yRotationRoot.localEulerAngles.y < minMaxYRotation.x) yAxisPositive = true;
        if(yRotationRoot.localEulerAngles.y > minMaxYRotation.y) yAxisPositive = false;
        yRotationRoot.localRotation = Quaternion.Euler(new Vector3(0f, AddRotation(yAxisPositive, yRotationRoot.localEulerAngles.y), 0f));

        if (zRotationRoot.localEulerAngles.z < minMaxYRotation.x) zAxisPositive = true;
        if (zRotationRoot.localEulerAngles.z > minMaxYRotation.y) zAxisPositive = false;
        zRotationRoot.localRotation = Quaternion.Euler(new Vector3(0f, 90f, AddRotation(zAxisPositive, zRotationRoot.localEulerAngles.z)));
    }
}
