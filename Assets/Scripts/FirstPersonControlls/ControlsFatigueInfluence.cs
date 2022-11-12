using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ControlsFatigueInfluence : MonoBehaviour
{
    public int fatigueValue = 50;
    public FirstPersonController firstpersoncontroller;

    [Header("Noise parameters")]
    [SerializeField]
    private float sampleCount;
    [SerializeField]
    private float sampleStep;
    [SerializeField]
    private float noiseScaling;
    //[SerializeField]
    private List<float> noise = new List<float>();
    private int noiseArrayIndex = 0;

    [Header("Fatigue parameters")]
    [SerializeField]
    private float fatigueMovementInfluenceScaling = .1f;

    // Start is called before the first frame update
    void Start()
    {
       CalcPerlinNoise();
    }

    // Update is called once per frame
    void Update()
    {
        if (noiseArrayIndex >= noise.Count -1) noiseArrayIndex = 0;
        noiseArrayIndex++;
    }

    //movement speed gets adjusted when fatigue value changes
    public float GetFatigueValue() 
    {
        return PlayerStats.Instance.Fatigue * fatigueMovementInfluenceScaling;       
    }

    float xCoord = 0.0f;
    float yCoord = 0.0f;
    int arraycoord;

    public void CalcPerlinNoise() 
    {
        for (int i = 0; i < sampleCount * sampleStep; i++)
        {
            noise.Add((Mathf.PerlinNoise(0f, i / sampleStep) - .5f) * noiseScaling);
        }
    }

    public float GetFatigueNoiseInfluence() 
    {
        return noise[noiseArrayIndex];
    }
    
    
}
