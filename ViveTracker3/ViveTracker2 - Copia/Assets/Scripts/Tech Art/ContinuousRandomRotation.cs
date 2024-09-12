using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ContinuousRandomRotation : MonoBehaviour
{
    [SerializeField] private float maxRot;
    [SerializeField] private float duration;
    private Vector3 startRot;

    private void Awake() => startRot = transform.localRotation.eulerAngles;

    private void Start() => ChangeRotation();

    private void ChangeRotation()
    {
        transform.DOLocalRotate(new Vector3(0f, 0f, Random.Range(-1 * maxRot, maxRot)) + startRot, duration).SetEase(Ease.InOutQuad).OnComplete(() => 
        {
            transform.DOLocalRotate(startRot, duration).SetEase(Ease.InOutCubic).OnComplete(() => ChangeRotation());
        });
    }
}
