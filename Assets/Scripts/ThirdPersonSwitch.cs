using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThirdPersonSwitch : MonoBehaviour
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCam;

    // time in seconds for camera to move from 1st to 3rd person
    [SerializeField]
    private float transitionTime = 1.0f;

    [SerializeField]
    private GameObject fpCharacter;
    private Animator fpAnimator;
    private StarterAssets.StarterAssetsInputs inputScript;
    private bool firstPerson = true;
    private bool raving = false;
    
    // how much the player fatigues per second he is raving
    [SerializeField]
    private int ravingFatigueRate = 2;
    private float ravingTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        inputScript = this.GetComponent<StarterAssets.StarterAssetsInputs>();
        fpAnimator = fpCharacter.GetComponentInChildren<Animator>();
        fpCharacter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.F))
        // {
        //     Overdose();
        // }

        if(raving)
        {
            ravingTime += Time.deltaTime;
            if(ravingTime > 1.0f)
            {
                PlayerStats.Instance.IncreaseFatigue(ravingFatigueRate);
                ravingTime -= 1.0f;
            }
        }
    }

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    public void OnRave()
    {
        raving = !raving;
        firstPerson = raving ? false : true;

        if (!firstPerson)
        {
            inputScript.moveAllowed = false;
            fpCharacter.transform.SetPositionAndRotation(transform.position, transform.rotation);
            fpCharacter.SetActive(true);
        }

        float targetValue = firstPerson ? 0.0f : 5.0f;
        DOTween.To(() => virtualCam.GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>().CameraDistance,
            x => virtualCam.GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>().CameraDistance = x,
            targetValue, 1.0f).OnComplete(() =>
            {
                inputScript.moveAllowed = firstPerson;
                fpCharacter.SetActive(!firstPerson);
            });
    }
#endif

    public void FallAsleep()
    {
        firstPerson = false;
        inputScript.moveAllowed = false;
        fpCharacter.transform.SetPositionAndRotation(transform.position, transform.rotation);
        fpCharacter.SetActive(true);
        fpAnimator.SetBool("Sleeping", true);
        
        DOTween.To(() => virtualCam.GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>().CameraDistance,
            x => virtualCam.GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>().CameraDistance = x,
            5.0f, 1.0f).OnComplete(() =>
            {

            });
    }

    public void Overdose()
    {
        firstPerson = false;
        inputScript.moveAllowed = false;
        fpCharacter.transform.SetPositionAndRotation(transform.position, transform.rotation);
        fpCharacter.SetActive(true);
        fpAnimator.SetBool("Dying", true);
        
        DOTween.To(() => virtualCam.GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>().CameraDistance,
            x => virtualCam.GetCinemachineComponent<Cinemachine.Cinemachine3rdPersonFollow>().CameraDistance = x,
            5.0f, 1.0f).OnComplete(() =>
            {

            });
    }
}
