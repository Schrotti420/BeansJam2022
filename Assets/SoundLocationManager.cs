using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLocationManager : MonoBehaviour
{
    public static SoundLocationManager Instance;
    public List<SoundStageManager> stageManagers;
    private void Awake()
    {
        Instance = this;
    }
    public void SwitchStage(string stageName)
    {
        foreach(SoundStageManager stage in stageManagers)
        {
            if (stage.gameObject.name.Contains(stageName)) stage.ActivateSound();
            else stage.DeactivateSound();
        }
    }
}
