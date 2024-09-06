using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class JellyfishStart : MonoBehaviour
{
    [SerializeField] GameObject Instrucoes;
    [SerializeField] GameObject Canvas;

    public void Collect()
    {
        Instrucoes.SetActive(false);
        Canvas.SetActive(true);
    }
}
