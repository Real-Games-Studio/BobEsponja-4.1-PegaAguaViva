using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnEnable : MonoBehaviour
{
    [SerializeField] AudioSource audioSource; 
    void OnEnable()
    {
        audioSource.Play();
    }
}
