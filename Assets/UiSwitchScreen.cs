using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiSwitchScreen : MonoBehaviour
{
    public RectTransform currentScreen;
    [SerializeField]
    private float transitionDuration;
    [SerializeField]
    private RectTransform belowPosition, abovePosition, centerPosition;
    [SerializeField]
    private GameObject pauseMenuRoot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void SwitchScreen(RectTransform newScreen)
    {
        currentScreen.DOMove(abovePosition.position, transitionDuration);
        newScreen.position = belowPosition.position;
        newScreen.DOMove(centerPosition.position, transitionDuration);
        currentScreen = newScreen;
    }
    public void HideShowUI()
    {
        pauseMenuRoot.SetActive(!pauseMenuRoot.activeSelf);
    }

    public void OnPauseMenu(InputValue value)
    {
        Debug.Log("Escape was pressed!");
        HideShowUI();
        Cursor.visible = pauseMenuRoot.activeSelf;
        if(!pauseMenuRoot.activeSelf) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
    }
}
