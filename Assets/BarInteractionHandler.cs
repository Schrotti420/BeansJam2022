using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BarInteractionHandler : MonoBehaviour
{
    public static BarInteractionHandler Instance;
    [SerializeField]
    GameObject beerUIRoot;
    public List<Button> barUiButtons;

    public void Start()
    {
        Instance = this;
    }

    [SerializeField]
    private int beerInfluenceOnOverdose, waterInfluenceOnFatigue;
    public void DrinkBeer()
    {
        if(PlayerStats.Instance.Fatigue > 60)
            PlayerStats.Instance.IncreaseOverdose(beerInfluenceOnOverdose);
        else
            PlayerStats.Instance.IncreaseFatigue(beerInfluenceOnOverdose);

        PlayerStats.Instance.attentionStatus(0f, false, 15f);
    }
    public void DrinkWater()
    {
        if(PlayerStats.Instance.Fatigue > 50)
            PlayerStats.Instance.IncreaseFatigue(waterInfluenceOnFatigue);
        else
            PlayerStats.Instance.IncreaseOverdose(waterInfluenceOnFatigue);

        PlayerStats.Instance.attentionStatus(2f, true, 15f);
    }
    public void Show()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        beerUIRoot.SetActive(true);
    }
    public void Hide(float delay)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(HideDelayCoroutine(delay));
    }
    IEnumerator HideDelayCoroutine(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        beerUIRoot.SetActive(false);
    }
    private void ResetButtons()
    {
        foreach(Button button in barUiButtons)
        {
            button.enabled = false;
            button.enabled = true;
        }
    }
}
