using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleManager : MonoBehaviour
{
    public bool[] controllersIdle;
    public Idle idle;

    void FixedUpdate()
    {
        if (AllTrue(controllersIdle))
        {
            idle.ResetGame();
        }
    }

    bool AllTrue(bool[] array)
    {
        foreach (bool element in array)
        {
            if (!element)
            {
                return false;
            }
        }
        return true;
    }
}
