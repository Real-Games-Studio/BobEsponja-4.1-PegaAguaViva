using DG.Tweening;
using UnityEngine;

public class Popup : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOScale(1.25f, 0.1f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            transform.DOScale(1f, 0.2f).SetEase(Ease.InOutQuad);
        });
    }
}
