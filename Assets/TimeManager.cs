using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [SerializeField]
    private SceneLoadingStruct winScreen;
    [SerializeField]
    private SceneLoadingStruct asleepScreen;
    [SerializeField]
    private SceneLoadingStruct caughtScreen;
    [SerializeField]
    private SceneLoadingStruct overdoseScreen;
    [SerializeField]
    float maxGameTimeInS;
    float overallGameTime;
    [SerializeField]
    int hourInClub;

    float hourPointerOverallRotation;
    float minutePointerOverallRotation;

    Vector3 initialHourRotation, initialMinuteRotation;

    [SerializeField]
    RectTransform hoursPointer, minutesPointer;
    bool isWon = false;
    bool isLost = false;

    private void Start()
    {
        Instance = this;
        overallGameTime = maxGameTimeInS;
        hourPointerOverallRotation = hourInClub * 30;
        minutePointerOverallRotation = hourInClub * 360;

        initialHourRotation = hoursPointer.localRotation.eulerAngles;
        initialMinuteRotation = minutesPointer.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        maxGameTimeInS -= Time.deltaTime;

        if (maxGameTimeInS > 0 && !isWon && !isLost)
        {
            AnimateClock();
        }
        else if (!isWon && maxGameTimeInS <= 0)
        {
            isWon = true;
            ShowWinScreen();
        }
    }
    private void AnimateClock()
    {
        float currentHoursRotation = (maxGameTimeInS / overallGameTime) * hourPointerOverallRotation;
        hoursPointer.localRotation = Quaternion.Euler(0f, 0f, initialHourRotation.z + currentHoursRotation);

        float currentMinutesRotation = (maxGameTimeInS / overallGameTime) * minutePointerOverallRotation;
        minutesPointer.localRotation = Quaternion.Euler(0f, 0f, initialMinuteRotation.z + currentMinutesRotation);
    }
    private void ShowWinScreen()
    {
        UnlockCursor();
        SceneManager.LoadScene(winScreen.sceneName, winScreen.loadSceneMode);
    }
    public void ShowOverdoseScreen()
    {
        UnlockCursor();
        if (isWon) return;
        isLost = true;
        SceneManager.LoadScene(overdoseScreen.sceneName, overdoseScreen.loadSceneMode);
    }
    public void ShowAsleepScreen()
    {
        UnlockCursor();
        if (isWon) return;
        isLost = true;
        SceneManager.LoadScene(asleepScreen.sceneName, asleepScreen.loadSceneMode);
    }
    public void ShowCaughtScreen()
    {
        UnlockCursor();
        if (isWon) return;
        isLost = true;
        SceneManager.LoadScene(caughtScreen.sceneName, caughtScreen.loadSceneMode);
    }
    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
