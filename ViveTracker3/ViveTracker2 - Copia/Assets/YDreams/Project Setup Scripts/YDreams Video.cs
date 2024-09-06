using UnityEngine;
using UnityEngine.Video;
using System.IO;
using RenderHeads.Media.AVProVideo;

public class YDreamsVideo : MonoBehaviour
{
    [SerializeField] MediaReference backupVideo;

    private void Start()
    {
        if (File.Exists(Path.Combine("contents/videos", gameObject.name + ".mp4")))
        {
            ProjectSetup.LoadAVProVideo(gameObject.name + ".mp4", GetComponent<MediaPlayer>());
        }
        else if (File.Exists(Path.Combine("contents/videos", gameObject.name + ".webm")))
        {
            ProjectSetup.LoadAVProVideo(gameObject.name + ".webm", GetComponent<MediaPlayer>());
        }
        else if (backupVideo != null)
        {
            ProjectSetup.LoadAVProVideo("ReturnError.mp4", GetComponent<MediaPlayer>(), backupVideo);
        }
        else
        {
            ProjectSetup.VideoNotFound(GetComponent<MediaPlayer>());
        }
    }
}
