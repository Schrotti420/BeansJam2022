using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManipulation : MonoBehaviour
{
    [Header("Scaling parameters")]
    public float vignetteScaling;
    public float tintLerpDuration;
    public float reverseDuration;
    public float exposureScaling;

    [Header("Performance parameters")]
    [SerializeField]
    float skippedFrames;
    float m_frameCounter;

    float m_overdose, m_fatigue;
    int m_tintValue;
    int m_hueValue;
    Sequence tintSequence;
    Sequence hueSequence;

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
        colorGrading.tint.Override(0f);

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

            AdjustColorgrading(m_overdose);
            colorGrading.tint.value = m_tintValue;
            colorGrading.hueShift.value = m_hueValue;

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

    void AdjustColorgrading(float overdoseVal)
    {
        if(overdoseVal > .75f)
        {
            if (!tintSequence.IsPlaying())
            {
                tintSequence = DOTween.Sequence();
                tintSequence.Append(DOTween.To(() => m_tintValue, x => m_tintValue = x, 100, tintLerpDuration));
                tintSequence.Append(DOTween.To(() => m_tintValue, x => m_tintValue = x, -100, tintLerpDuration));
                tintSequence.SetLoops(-1, LoopType.Restart);
            }
            if (!hueSequence.IsPlaying())
            {
                hueSequence = DOTween.Sequence();
                hueSequence.Append(DOTween.To(() => m_hueValue, x => m_hueValue = x, 180, tintLerpDuration));
                hueSequence.Append(DOTween.To(() => m_hueValue, x => m_hueValue = x, -180, tintLerpDuration));
                hueSequence.SetLoops(-1, LoopType.Restart);
            }
        }
        else
        {
            if(tintSequence != null) tintSequence.Kill();
            if(m_tintValue != 0) DOTween.To(() => m_tintValue, x => m_tintValue = x, 0, reverseDuration);

            if(hueSequence != null) hueSequence.Kill();
            if(m_hueValue != 0) DOTween.To(() => m_tintValue, x => m_tintValue = x, 0, reverseDuration);
        }
        colorGrading.postExposure.value = overdoseVal * exposureScaling;
    }
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
