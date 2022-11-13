using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BarInteractionHandler : MonoBehaviour
{
    public static BarInteractionHandler Instance;
    [SerializeField]
    GameObject beerUIRoot;

    public void Start()
    {
        Instance = this;
    }

    [SerializeField]
    private int beerInfluenceOnOverdose, waterInfluenceOnFatigue;
    public void DrinkBeer()
    {
        PlayerStats.Instance.IncreaseOverdose(beerInfluenceOnOverdose);
        PlayerStats.Instance.attentionStatus(0f, false, 15f);
    }
    public void DrinkWater()
    {
        PlayerStats.Instance.IncreaseFatigue(waterInfluenceOnFatigue);
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
}
