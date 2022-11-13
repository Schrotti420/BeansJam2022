using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterSceneManager : MonoBehaviour
{
    public static MasterSceneManager Instance;

    public List<SceneLoadingStruct> gameLoopScenes;
    public List<SceneLoadingStruct> mainMenueScenes;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }
    public void LoadGame()
    {
        foreach(SceneLoadingStruct sceneLoadingStruct in gameLoopScenes)
        {
            SceneManager.LoadScene(sceneLoadingStruct.sceneName, sceneLoadingStruct.loadSceneMode);
        }
    }
    public void LoadMainMenue()
    {
        foreach (SceneLoadingStruct sceneLoadingStruct in mainMenueScenes)
        {
            SceneManager.LoadScene(sceneLoadingStruct.sceneName, sceneLoadingStruct.loadSceneMode);
        }
    }
    public void ExitGame()
    {
        throw new NotImplementedException();
    }
}
[System.Serializable]
public struct SceneLoadingStruct
{
    public string sceneName;
    public LoadSceneMode loadSceneMode; 
}
