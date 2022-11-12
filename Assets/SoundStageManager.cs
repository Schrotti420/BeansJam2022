using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using DG.Tweening;

public class SoundStageManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource primary, secondary;
    [SerializeField]
    private float fadeThresholdInS = 6.857f;
    [SerializeField]
    private List<AudioClip> tracks;
    private int currentTrack, previousTrack;
    private bool isPrimaryActive = true;

    private void Start()
    {
        primary.clip = tracks[0];
        currentTrack = 0;
        primary.Play();

        StartCoroutine(FadeManagerDaemon());
    }
    private int GetNextTrack(int currentTrack)
    {
        int next;
        do
        {
            next = Random.Range(0, tracks.Count);
        } while (next != currentTrack);
        return next;
    }
    private AudioSource GetActiveSource()
    {
        if (primary.isPlaying) return primary;
        if (secondary.isPlaying) return secondary;
        return null;
    }
    private IEnumerator FadeManagerDaemon()
    {
        while (true)
        {
            AudioSource active = GetActiveSource();
            if (active == null) Debug.LogError("FUCK!");
            yield return null;
            while (active.isPlaying)
            {
                Debug.Log("Active Source is playing");
                //Debug.Log($"Active delta is {active.clip.length - active.time}");
                if (active.time > active.clip.length - fadeThresholdInS)
                {
                    //fade
                    previousTrack = currentTrack;
                    currentTrack = GetNextTrack(previousTrack);
                    Debug.Log($"Fading tracks, new track is: {currentTrack}");

                    if (isPrimaryActive)
                    {
                        secondary.clip = tracks[currentTrack];
                        secondary.Play();
                        primary.DOFade(0f, fadeThresholdInS);
                        secondary.DOFade(1f, fadeThresholdInS);
                        isPrimaryActive = false;
                    }
                    else
                    {
                        primary.clip = tracks[currentTrack];
                        primary.Play();
                        secondary.DOFade(0f, fadeThresholdInS);
                        primary.DOFade(1f, fadeThresholdInS);
                        isPrimaryActive = true;
                    }
                    yield return null;
                    active = GetActiveSource();
                    break;
                }
                yield return null;
            }
        }
    }
}
