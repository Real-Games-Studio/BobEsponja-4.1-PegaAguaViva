using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DebugObject : MonoBehaviour
{
    [HideInInspector] public GameObject CanvasDebugger;

    Vector3 StartPosition;
    Vector3 StartRotation;
    Vector3 StartScale;

    [HideInInspector] public TMP_InputField PosX;
    [HideInInspector] public TMP_InputField PosY;
    [HideInInspector] public TMP_InputField PosZ;

    [HideInInspector] public TMP_InputField RotX;
    [HideInInspector] public TMP_InputField RotY;
    [HideInInspector] public TMP_InputField RotZ;

    [HideInInspector] public TMP_InputField ScaleX;
    [HideInInspector] public TMP_InputField ScaleY;
    [HideInInspector] public TMP_InputField ScaleZ;

    private void Start()
    {
        StartPosition = transform.position;
        StartRotation = transform.localEulerAngles;
        StartScale = transform.localScale;

        GetInputs();
        PullSave();
        AddListenersToInput();

        if(gameObject.name == "Camera")
        {
            GetComponent<Camera>().orthographicSize = JSONFile.Configclass.tamanhoCamera;
        }
    }

    void ChangePosition()
    {
        float x, y, z;

        if (float.TryParse(PosX.text, out x) && float.TryParse(PosY.text, out y) && float.TryParse(PosZ.text, out z))
        {
            transform.position = StartPosition + new Vector3(x, y, z);
            PlayerPrefs.SetString(gameObject.name + "PosX", PosX.text);
            PlayerPrefs.SetString(gameObject.name + "PosY", PosY.text);
            PlayerPrefs.SetString(gameObject.name + "PosZ", PosZ.text);
        }
    }

    void ChangeRotation()
    {
        float x, y, z;

        if (float.TryParse(RotX.text, out x) && float.TryParse(RotY.text, out y) && float.TryParse(RotZ.text, out z))
        {
            transform.eulerAngles = StartRotation + new Vector3(x, y, z);
            PlayerPrefs.SetString(gameObject.name + "RotX", RotX.text);
            PlayerPrefs.SetString(gameObject.name + "RotY", RotY.text);
            PlayerPrefs.SetString(gameObject.name + "RotZ", RotZ.text);
        }
    }

    void ChangeScale()
    {
        float x, y, z;

        if (float.TryParse(ScaleX.text, out x) && float.TryParse(ScaleY.text, out y) && float.TryParse(ScaleZ.text, out z))
        {
            transform.localScale = StartScale + new Vector3(x, y, z);
            PlayerPrefs.SetString(gameObject.name + "ScaleX", ScaleX.text);
            PlayerPrefs.SetString(gameObject.name + "ScaleY", ScaleY.text);
            PlayerPrefs.SetString(gameObject.name + "ScaleZ", ScaleZ.text);
        }
    }

    void GetInputs()
    {
        PosX = CanvasDebugger.transform.Find("Position").Find("PosX").GetComponent<TMP_InputField>();
        PosY = CanvasDebugger.transform.Find("Position").Find("PosY").GetComponent<TMP_InputField>();
        PosZ = CanvasDebugger.transform.Find("Position").Find("PosZ").GetComponent<TMP_InputField>();

        RotX = CanvasDebugger.transform.Find("Rotation").Find("RotX").GetComponent<TMP_InputField>();
        RotY = CanvasDebugger.transform.Find("Rotation").Find("RotY").GetComponent<TMP_InputField>();
        RotZ = CanvasDebugger.transform.Find("Rotation").Find("RotZ").GetComponent<TMP_InputField>();

        ScaleX = CanvasDebugger.transform.Find("Scale").Find("ScaleX").GetComponent<TMP_InputField>();
        ScaleY = CanvasDebugger.transform.Find("Scale").Find("ScaleY").GetComponent<TMP_InputField>();
        ScaleZ = CanvasDebugger.transform.Find("Scale").Find("ScaleZ").GetComponent<TMP_InputField>();
    }

    void AddListenersToInput()
    {
        PosX.onValueChanged.AddListener(delegate { ChangePosition(); });
        PosY.onValueChanged.AddListener(delegate { ChangePosition(); });
        PosZ.onValueChanged.AddListener(delegate { ChangePosition(); });

        RotX.onValueChanged.AddListener(delegate { ChangeRotation(); });
        RotY.onValueChanged.AddListener(delegate { ChangeRotation(); });
        RotZ.onValueChanged.AddListener(delegate { ChangeRotation(); });

        ScaleX.onValueChanged.AddListener(delegate { ChangeScale(); });
        ScaleY.onValueChanged.AddListener(delegate { ChangeScale(); });
        ScaleZ.onValueChanged.AddListener(delegate { ChangeScale(); });
    }

    void PullSave()
    {
        PosX.text = PlayerPrefs.GetString(gameObject.name + "PosX");
        Debug.Log(PlayerPrefs.GetString(gameObject.name + "PosX"));
        PosY.text = PlayerPrefs.GetString(gameObject.name + "PosY");
        PosZ.text = PlayerPrefs.GetString(gameObject.name + "PosZ");
        ChangePosition();

        RotX.text = PlayerPrefs.GetString(gameObject.name + "RotX");
        RotY.text = PlayerPrefs.GetString(gameObject.name + "RotY");
        RotZ.text = PlayerPrefs.GetString(gameObject.name + "RotZ");
        ChangeRotation();

        ScaleX.text = PlayerPrefs.GetString(gameObject.name + "ScaleX");
        ScaleY.text = PlayerPrefs.GetString(gameObject.name + "ScaleY");
        ScaleZ.text = PlayerPrefs.GetString(gameObject.name + "ScaleZ");
        ChangeScale();
    }

    
}
