using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetColors : MonoBehaviour
{
    public GameObject rede1;
    public GameObject rede2;
    public GameObject rede3;
    public GameObject rede4;

    void Start()
    {
        Color rede1color;
        Color rede2color;
        Color rede3color;
        Color rede4color;

        if(ColorUtility.TryParseHtmlString(JSONFile.Configclass.CorRede1, out rede1color))
        {
            rede1.GetComponent<Renderer>().sharedMaterial.color = rede1color;
        }
        else
        {
           Debug.LogError("Cor inválida para a rede 1");
        }

        if(ColorUtility.TryParseHtmlString(JSONFile.Configclass.CorRede2, out rede2color))
        {
            rede2.GetComponent<Renderer>().sharedMaterial.color = rede2color;
        }
        else
        {
           Debug.LogError("Cor inválida para a rede 2");
        }

        if(ColorUtility.TryParseHtmlString(JSONFile.Configclass.CorRede3, out rede3color))
        {
            rede3.GetComponent<Renderer>().sharedMaterial.color = rede3color;
        }
        else
        {
           Debug.LogError("Cor inválida para a rede 3");
        }

        if(ColorUtility.TryParseHtmlString(JSONFile.Configclass.CorRede4, out rede4color))
        {
            rede4.GetComponent<Renderer>().sharedMaterial.color = rede4color;
        }
        else
        {
           Debug.LogError("Cor inválida para a rede 4");
        }
    }
}
