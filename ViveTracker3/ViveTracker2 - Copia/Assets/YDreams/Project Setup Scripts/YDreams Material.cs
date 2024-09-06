using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class YDreamsMaterial : MonoBehaviour
{
    [SerializeField] Material backupMaterial;

    private void Start()
    {
        if (File.Exists(Path.Combine("contents/images", gameObject.name + ".png")))
        {
            ProjectSetup.LoadMaterial(gameObject.name + ".png", GetComponent<Renderer>());
        }
        else if(backupMaterial != null)
        {
            ProjectSetup.LoadMaterial(gameObject.name + ".png", GetComponent<Renderer>(), backupMaterial);
        }
        else
        {
            ProjectSetup.LoadMaterial("ReturnError.png", GetComponent<Renderer>());
        }
    }
}
