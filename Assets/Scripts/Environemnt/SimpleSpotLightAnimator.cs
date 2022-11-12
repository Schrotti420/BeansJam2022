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

    private bool yAxisPositive, zAxisPositive;

    // Update is called once per frame
    void Update()
    {
        yRotationRoot.localRotation = Quaternion.Euler(new Vector3(0f, AddRotation(IsAxisDirectionPositive(minMaxYRotation, yRotationRoot.localEulerAngles.y), yRotationRoot.localEulerAngles.y), 0f));
        zRotationRoot.localRotation = Quaternion.Euler(new Vector3(0f, 90f, AddRotation(IsAxisDirectionPositive(minMaxZRotation, zRotationRoot.localEulerAngles.z), zRotationRoot.localEulerAngles.y)));
    }
    private bool IsAxisDirectionPositive(Vector2 minMax, float rotation)
    {
        if(rotation < minMax.x) return true;
        else return false;
    }
    private float AddRotation(bool isPositiveDirection, float rotation)
    {
        if (isPositiveDirection) return rotation + (degreesPerSecond * Time.deltaTime);
        else return rotation - (degreesPerSecond * Time.deltaTime);
    }
}
