using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManipulation : MonoBehaviour
{
    [Header("Scaling parameters")]
    float vignetteScaling;

    [Header("Performance parameters")]
    [SerializeField]
    float skippedFrames;
    float m_frameCounter;

    float m_overdose, m_fatigue;

    PostProcessVolume m_Volume;
    Vignette vignette;
    MotionBlur motionBlur;
    ColorGrading colorGrading;
    LensDistortion lensDistortion;
    Bloom bloom;

    private void Start()
    {
        m_frameCounter = skippedFrames;

        vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.enabled.Override(true);

        motionBlur = ScriptableObject.CreateInstance<MotionBlur>();
        motionBlur.enabled.Override(true);

        colorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        colorGrading.enabled.Override(true);

        lensDistortion = ScriptableObject.CreateInstance<LensDistortion>();
        lensDistortion.enabled.Override(true);

        bloom = ScriptableObject.CreateInstance<Bloom>();
        bloom.enabled.Override(true);

        PostProcessEffectSettings[] settings = new PostProcessEffectSettings[]
        {
            vignette,
            motionBlur,
            colorGrading,
            lensDistortion,
            bloom
        };

        InitializeEffects();

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, settings);
    }

    private void InitializeEffects()
    {
        vignette.intensity.Override(1f);

        colorGrading.gradingMode.Override(GradingMode.HighDefinitionRange);
        colorGrading.tonemapper.Override(Tonemapper.ACES);

        lensDistortion.intensity.Override(0f);

        bloom.intensity.Override(0f);
    }
    void Update()
    {
        if(m_frameCounter != 0)
        {
            m_frameCounter--;
        }
        else
        {
            //calculate overdose and fatigue value [0-1]
            CalculateOverdoseFatigue();

            //edit effects
            AdjustVignette(m_fatigue);

            m_frameCounter = skippedFrames;
        }
    }
    void CalculateOverdoseFatigue()
    {
        m_overdose = Mathf.Clamp((PlayerStats.Instance.Fatigue - 50 / 50), 0f, 1f);
        m_fatigue = Mathf.Clamp((PlayerStats.Instance.Fatigue +50 / 50), 0f, 1f);
    }
    void AdjustVignette(float fatigueval)
    {
        vignette.intensity.value = fatigueval * vignetteScaling;
    }
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
