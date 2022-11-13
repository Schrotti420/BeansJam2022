using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private SceneLoadingStruct winScreen;
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

    private void Start()
    {
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

        if(maxGameTimeInS > 0 && !isWon)
        {
            AnimateClock();
        }
        else
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
        SceneManager.LoadScene(winScreen.sceneName, winScreen.loadSceneMode);
    }
}
