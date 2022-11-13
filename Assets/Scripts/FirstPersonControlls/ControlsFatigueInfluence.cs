using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ControlsFatigueInfluence : MonoBehaviour
{
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
    private float m_overdose;
    private float m_fatigue;
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
        CalculateOverdoseFatigue();
        float random = Random.value - 1;
        return m_fatigue * fatigueMovementInfluenceScaling * random;       
    }
    void CalculateOverdoseFatigue()
    {
        //m_overdose = PlayerStats.Instance.Fatigue > 50 ? Mathf.Clamp(((PlayerStats.Instance.Fatigue - 50) / 50f), 0f, 1f) : 0f;
        m_overdose = PlayerStats.Instance.Fatigue > 50 ? map(PlayerStats.Instance.Fatigue, 50, 0, 0, 1) : 0;
        //m_fatigue = PlayerStats.Instance.Fatigue < 50 ? Mathf.Clamp(((PlayerStats.Instance.Fatigue) / 50f), 0f, 1f) : 0f;
        m_fatigue = PlayerStats.Instance.Fatigue < 50 ? map(PlayerStats.Instance.Fatigue, 50, 100, 0, 1) : 0;
    }
    float map(float value, float low1, float high1, float low2, float high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1) * -1;
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
        return noise[noiseArrayIndex] * GetFatigueValue();
    }
    
    
}
