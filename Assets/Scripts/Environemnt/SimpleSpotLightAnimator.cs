using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpotLightAnimator : MonoBehaviour
{
    public float yDuration, zDuration;
    [SerializeField]
    Transform yRotationRoot;
    [SerializeField]
    Transform zRotationRoot;

    private Vector3 initialY, initialZ;

    private void Start()
    {
        StartCoroutine(StartSpotlightAnimation());
    }
    private IEnumerator StartSpotlightAnimation()
    {
        yield return new WaitForSecondsRealtime(Random.value * 2);

        Sequence zse = DOTween.Sequence();
        Sequence yse = DOTween.Sequence();
        zse.Append(zRotationRoot.DOLocalRotate(new Vector3(0f,90f, 50f), zDuration + Random.value, RotateMode.Fast).SetEase(Ease.Linear));
        zse.Append(zRotationRoot.DOLocalRotate(new Vector3(0f, 90f, 150f), zDuration + Random.value, RotateMode.Fast).SetEase(Ease.Linear));
        //yse.Append(yRotationRoot.DOLocalRotate(new Vector3(0f, 70, 0f), yDuration + Random.value, RotateMode.LocalAxisAdd));
        //yse.Append(yRotationRoot.DOLocalRotate(new Vector3(0f, 0f, 0f), yDuration + Random.value, RotateMode.Fast));

        zse.SetLoops(-1, LoopType.Yoyo);
        yse.SetLoops(-1, LoopType.Yoyo);

        zse.Play();
        yse.Play();
    }
}
