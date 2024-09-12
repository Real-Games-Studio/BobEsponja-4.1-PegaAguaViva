using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScaleToScale : MonoBehaviour
{
    public Vector3 startScale;
    public Vector3 endScale;
    public float interval;
    public float intialDelay;
    public Ease ease =  Ease.InOutQuad;

    private void OnEnable()
    {
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        transform.localScale = startScale;

        yield return new WaitForSeconds(intialDelay);

        transform.DOScale(endScale, interval).SetEase(ease);
    }
}
