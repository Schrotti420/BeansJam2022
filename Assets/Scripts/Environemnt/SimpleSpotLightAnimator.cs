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
        StartSpotlightAnimation();
    }
    private void StartSpotlightAnimation()
    {
        Sequence zse = DOTween.Sequence();
        Sequence yse = DOTween.Sequence();
        zse.Append(zRotationRoot.DOLocalRotate(new Vector3(0f,90f,-180f), zDuration + Random.value, RotateMode.LocalAxisAdd));
        zse.Append(zRotationRoot.DOLocalRotate(new Vector3(0f, 90f, 180f), zDuration + Random.value, RotateMode.LocalAxisAdd));
        yse.Append(yRotationRoot.DOLocalRotate(new Vector3(0f, 70, 0f), yDuration + Random.value, RotateMode.LocalAxisAdd));
        yse.Append(yRotationRoot.DOLocalRotate(new Vector3(0f, 0f, 0f), yDuration + Random.value, RotateMode.LocalAxisAdd));

        zse.SetLoops(-1, LoopType.Yoyo);
        yse.SetLoops(-1, LoopType.Yoyo);

        zse.Play();
        yse.Play();
    }
}
