using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetMasterVolume : MonoBehaviour
{
    public AudioMixer masterMixer;
    private float sliderVolume;
    public Slider slider;

    private void OnEnable()
    {
        masterMixer.GetFloat("MasterVolume", out sliderVolume);
        slider.value = sliderVolume;
    }
    public void Override(float volume)
    {
        masterMixer.SetFloat("MasterVolume", volume);
    }
}
