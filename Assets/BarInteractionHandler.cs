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
    }
    public void DrinkWater()
    {
        PlayerStats.Instance.IncreaseFatigue(waterInfluenceOnFatigue);
    }
    public void Show()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        beerUIRoot.SetActive(true);
    }
    public void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        beerUIRoot.SetActive(false);
    }
}
