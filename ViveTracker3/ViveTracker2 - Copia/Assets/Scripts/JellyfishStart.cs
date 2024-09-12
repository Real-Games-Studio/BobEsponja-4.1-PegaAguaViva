using System.Collections;
using UnityEngine;

public class JellyfishStart : MonoBehaviour
{
    [SerializeField] GameObject Instrucoes;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject particle;

    public void Collect()
    {
        StartCoroutine(CollectTransition());
    }

    public IEnumerator CollectTransition()
    {
        particle.SetActive(true);
        Instrucoes.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(.5f);
        Instrucoes.SetActive(false);
        Canvas.SetActive(true);
        yield return new WaitForSeconds(.5f);
        particle.SetActive(false);
    }
}
