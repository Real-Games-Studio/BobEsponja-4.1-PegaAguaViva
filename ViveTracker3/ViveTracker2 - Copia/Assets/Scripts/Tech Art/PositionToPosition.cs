using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PositionToPosition : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float interval;

    private void OnEnable()
    {
        transform.localPosition = startPos;

        transform.DOLocalMove(endPos, interval).SetEase(Ease.InOutQuad);
    }
}
