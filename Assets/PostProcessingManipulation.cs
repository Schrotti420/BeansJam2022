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
    [Range(0f, 1f)]
    public float vignetteScaling;
    [Range(0f, 8f)]
    public float tintLerpDuration;
    [Range(0f, 8f)]
    public float lensDistortionDuration;
    [Range(0f, 8f)]
    public float reverseDuration;
    [Range(0f, 2f)]
    public float exposureScaling;
    [Range(0, 30)]
    public float defaultBloom;
    [Range(0, 50)]
    public float bloomScaling;
    [Range(0, 5f)]
    public float chromaticAberrationScaling;

    [Header("Performance parameters")]
    [SerializeField]
    float skippedFrames;
    float m_frameCounter;

    float m_overdose, m_fatigue;
    int m_tintValue;
    int m_hueValue;
    int m_lensDistValue;
    Sequence tintSequence;
    Sequence hueSequence;
    Sequence lensSequence;

    PostProcessVolume m_Volume;
    Vignette vignette;
    MotionBlur motionBlur;
    ColorGrading colorGrading;
    LensDistortion lensDistortion;
    Bloom bloom;
    ChromaticAberration chromaticAberration;

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
        bloom.fastMode.Override(true);

        chromaticAberration = ScriptableObject.CreateInstance<ChromaticAberration>();
        chromaticAberration.enabled.Override(true);
        chromaticAberration.intensity.Override(0f);

        PostProcessEffectSettings[] settings = new PostProcessEffectSettings[]
        {
            vignette,
            motionBlur,
            colorGrading,
            lensDistortion,
            bloom,
            chromaticAberration
        };

        InitializeEffects();

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, settings);
    }

    private void InitializeEffects()
    {
        vignette.intensity.Override(0f);

        colorGrading.gradingMode.Override(GradingMode.HighDefinitionRange);
        colorGrading.tonemapper.Override(Tonemapper.ACES);
        colorGrading.tint.Override(0f);
        colorGrading.hueShift.Override(0f);
        colorGrading.saturation.Override(0f);

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

            AdjustColorgrading(m_overdose, m_fatigue);
            colorGrading.tint.value = m_tintValue;
            colorGrading.hueShift.value = m_hueValue;

            AdjustLensDistortion(m_overdose);
            lensDistortion.intensity.value = m_lensDistValue;

            AdjustBloom(m_overdose);

            AdjustChromaticAberration(m_overdose);

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

    void AdjustColorgrading(float overdoseVal, float fatigueVal)
    {
        if(fatigueVal > 0f)
        {
            colorGrading.saturation.value = fatigueVal;
        }
        if(overdoseVal > .5f)
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
            if(tintSequence.IsActive()) tintSequence.Kill();
            if(m_tintValue != 0) DOTween.To(() => m_tintValue, x => m_tintValue = x, 0, reverseDuration);

            if(hueSequence.IsActive()) hueSequence.Kill();
            if(m_hueValue != 0) DOTween.To(() => m_hueValue, x => m_hueValue = x, 0, reverseDuration);
        }
        colorGrading.postExposure.value = overdoseVal * exposureScaling;
    }
    void AdjustLensDistortion(float overdoseVal)
    {
        if (overdoseVal > .75f)
        {
            if (!lensSequence.IsPlaying())
            {
                lensSequence = DOTween.Sequence();
                lensSequence.Append(DOTween.To(() => m_lensDistValue, x => m_lensDistValue = x, 20, lensDistortionDuration));
                lensSequence.Append(DOTween.To(() => m_lensDistValue, x => m_lensDistValue = x, -20, lensDistortionDuration));
                lensSequence.SetLoops(-1, LoopType.Restart);
            }
        }
        else
        {
            if (lensSequence.IsActive()) lensSequence.Kill();
            if (m_lensDistValue != 0) DOTween.To(() => m_lensDistValue, x => m_lensDistValue = x, 0, reverseDuration);
        }
    }
    void AdjustBloom(float overdoseVal)
    {
        bloom.intensity.value = overdoseVal * bloomScaling + defaultBloom;
    }
    void AdjustChromaticAberration(float overdoseVal)
    {
        chromaticAberration.intensity.value = overdoseVal * chromaticAberrationScaling;
    }
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
