using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ContinuousRandomScale : MonoBehaviour
{
    [SerializeField] private float maxScale;
    [SerializeField] private float duration;
    private float startScale;

    private void Awake() => startScale = transform.localScale.x;

    private void Start() => ChangeScale();

    private void ChangeScale()
    {
        transform.DOScale(Random.Range(startScale, maxScale) * Vector3.one, duration).SetEase(Ease.InOutQuad).OnComplete(() => 
        {
            transform.DOScale(startScale * Vector3.one, duration).SetEase(Ease.InOutCubic).OnComplete(() => ChangeScale());
        });
    }
}
