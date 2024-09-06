using UnityEngine;
using System.IO;

public class YDreamsAudio : MonoBehaviour
{
    [SerializeField] AudioClip backupAudio;

    private void Start()
    {
        if (File.Exists(Path.Combine("contents/audios", gameObject.name + ".mp3")))
        {
            StartCoroutine(ProjectSetup.LoadAudio(gameObject.name + ".mp3", GetComponent<AudioSource>()));
        }
        else if (File.Exists(Path.Combine("contents/audios", gameObject.name + ".wav")))
        {
            StartCoroutine(ProjectSetup.LoadAudio(gameObject.name + ".wav", GetComponent<AudioSource>()));
        }
        else if (backupAudio != null)
        {
            StartCoroutine(ProjectSetup.LoadAudio("ReturnError.mp3", GetComponent<AudioSource>(), backupAudio));
        }
        else
        {
            StartCoroutine(ProjectSetup.AudioNotFound(GetComponent<AudioSource>()));
        }
    }
}
