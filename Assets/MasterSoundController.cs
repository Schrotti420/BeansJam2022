using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MasterSoundController : MonoBehaviour
{
    public static MasterSoundController Instance;
    [SerializeField]
    private AudioMixer masterMixer;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }
    public void SetMasterVolume(float volume)
    {
        masterMixer.SetFloat("MasterVolume", volume);
    }
}
