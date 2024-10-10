using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetColors : MonoBehaviour
{
    PlayerController rede;
    bool done;

    void Update()
    {
        if (!done)
        {
            Color rede1color;
            Color rede2color;
            Color rede3color;
            Color rede4color;

            rede = GetComponent<PlayerController>();

            if (rede.TrackerID == 1 && rede.TrackerName != "")
            {
                if (ColorUtility.TryParseHtmlString(JSONFile.Configclass.CorRede1, out rede1color))
                {
                    GameObject.Find(rede.TrackerName).transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().sharedMaterial.color = rede1color;
                    done = true;
                }
                else
                {
                    Debug.LogError("Cor inválida para a rede 1");
                }
            }

            if (rede.TrackerID == 2 && rede.TrackerName != "")
            {
                if (ColorUtility.TryParseHtmlString(JSONFile.Configclass.CorRede2, out rede2color))
                {
                    GameObject.Find(rede.TrackerName).transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().sharedMaterial.color = rede2color;
                    done = true;
                }
                else
                {
                    Debug.LogError("Cor inválida para a rede 2");
                }
            }

            if (rede.TrackerID == 3 && rede.TrackerName != "")
            {
                if (ColorUtility.TryParseHtmlString(JSONFile.Configclass.CorRede3, out rede3color))
                {
                    GameObject.Find(rede.TrackerName).transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().sharedMaterial.color = rede3color;
                    done = true;
                }
                else
                {
                    Debug.LogError("Cor inválida para a rede 3");
                }
            }

            if (rede.TrackerID == 4 && rede.TrackerName != "")
            {
                if (ColorUtility.TryParseHtmlString(JSONFile.Configclass.CorRede4, out rede4color))
                {
                    GameObject.Find(rede.TrackerName).transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Renderer>().sharedMaterial.color = rede4color;
                    done = true;
                }
                else
                {
                    Debug.LogError("Cor inválida para a rede 4");
                }
            }
        }
    }
}
