using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickEvents: MonoBehaviour
{
    public GameObject startMenu;
    public GameObject settingsMenu;
    //quit application
    public void doExitGame()
    {
        Application.Quit();
    }

    //go to settings page
    public void settingsClick()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    //go from settings page back to main menu page
    public void backClick()
    {
        settingsMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    //adjust the ingame volume
    public void volumeAdjust()
    {

    }
}
