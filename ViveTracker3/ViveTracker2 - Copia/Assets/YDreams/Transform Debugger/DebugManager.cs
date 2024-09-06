using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugManager : MonoBehaviour
{
    [SerializeField] GameObject DebugProperties;
    [SerializeField] GameObject DebugPanel;

    [SerializeField] DebugObject[] ObjectsToDebug;

    private void Awake()
    {
        for(int i = 0; i< ObjectsToDebug.Length; i++)
        {
            GameObject debugcanvas = Instantiate(DebugProperties, DebugPanel.transform);
            ObjectsToDebug[i].GetComponent<DebugObject>().CanvasDebugger = debugcanvas;

            debugcanvas.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = ObjectsToDebug[i].name;
        }

        DebugPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DebugPanel.SetActive(!DebugPanel.activeSelf);
        }
    }
}
