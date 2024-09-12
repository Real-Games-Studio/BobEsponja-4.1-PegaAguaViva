using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ContinuousRandomPosition : MonoBehaviour
{
    [SerializeField] private Vector2 maxPos;
    [SerializeField] private float duration;
    private Vector3 startPos;

    private void Awake() => startPos = transform.localPosition;

    private void Start() => ChangePosition();

    private void ChangePosition()
    {
        transform.DOLocalMove(new Vector3(Random.insideUnitCircle.x * maxPos.x, Random.insideUnitCircle.y * maxPos.y, 0f) + startPos, duration).SetEase(Ease.InOutQuad).OnComplete(() => 
        {
            transform.DOLocalMove(startPos, duration).SetEase(Ease.InOutCubic).OnComplete(() => ChangePosition());
        });
    }
}
