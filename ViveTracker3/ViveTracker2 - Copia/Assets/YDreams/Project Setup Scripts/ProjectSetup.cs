using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Video;
using UnityEditor;
using RenderHeads.Media.AVProVideo;

public class ProjectSetup : MonoBehaviour
{
    void Awake()
    {
        UpdateFolders();
    }

    void UpdateFolders() 
    {
#if !UNITY_EDITOR
if (Directory.Exists("contents"))
        {
            
        }
        else
        {
            Directory.CreateDirectory("contents");
            Directory.CreateDirectory("contents/audios");
            Directory.CreateDirectory("contents/videos");
            Directory.CreateDirectory("contents/images");

            string[] filesA = Directory.GetFiles(Application.streamingAssetsPath + "/Audios");
            string[] filesB = Directory.GetFiles(Application.streamingAssetsPath + "/Videos");
            string[] filesC = Directory.GetFiles(Application.streamingAssetsPath + "/Images");

            foreach (string a in filesA)
            {
                string[] name = a.Split(@"\");
                string fileName = Path.GetFileName(a);
                File.Copy(a, Path.Combine("contents/audios", name[1]), true);
            }

            foreach (string b in filesB)
            {
                string[] name = b.Split(@"\");
                string fileName = Path.GetFileName(b);
                File.Copy(b, Path.Combine("contents/videos", name[1]), true);
            }

            foreach (string c in filesC)
            {
                string[] name = c.Split(@"\");
                string fileName = Path.GetFileName(c);
                File.Copy(c, Path.Combine("contents/images", name[1]), true);
            }

            File.Copy(Application.streamingAssetsPath + "/appconfig.json", "appconfig.json", true);

            Invoke("QuitApp", 5);
        }
#endif

#if UNITY_EDITOR
        if (Directory.Exists("contents"))
        {
            /*string[] filesA = Directory.GetFiles("contents/audios");
            string[] filesB = Directory.GetFiles("contents/videos");
            string[] filesC = Directory.GetFiles("contents/images");

            foreach (string a in filesA)
            {
                string[] name = a.Split(@"\");
                string fileName = Path.GetFileName(a);
                File.Copy(a, Path.Combine(Application.streamingAssetsPath + "/Audios", name[1]), true);
            }

            foreach (string b in filesB)
            {
                string[] name = b.Split(@"\");
                string fileName = Path.GetFileName(b);
                File.Copy(b, Path.Combine(Application.streamingAssetsPath + "/Videos", name[1]), true);
            }

            foreach (string c in filesC)
            {
                string[] name = c.Split(@"\");
                string fileName = Path.GetFileName(c);
                File.Copy(c, Path.Combine(Application.streamingAssetsPath + "/Images", name[1]), true);
            }

            File.Copy("appconfig.json", Application.streamingAssetsPath + "/appconfig.json", true);*/
        }
        else
        {
            Directory.CreateDirectory("contents");
            Directory.CreateDirectory("contents/audios");
            Directory.CreateDirectory("contents/videos");
            Directory.CreateDirectory("contents/images");

            string[] filesA = Directory.GetFiles(Application.streamingAssetsPath + "/Audios");
            string[] filesB = Directory.GetFiles(Application.streamingAssetsPath + "/Videos");
            string[] filesC = Directory.GetFiles(Application.streamingAssetsPath + "/Images");

            foreach (string a in filesA)
            {
                string[] name = a.Split(@"\");
                string fileName = Path.GetFileName(a);
                File.Copy(a, Path.Combine("contents/audios", name[1]), true);
            }

            foreach (string b in filesB)
            {
                string[] name = b.Split(@"\");
                string fileName = Path.GetFileName(b);
                File.Copy(b, Path.Combine("contents/videos", name[1]), true);
            }

            foreach (string c in filesC)
            {
                string[] name = c.Split(@"\");
                string fileName = Path.GetFileName(c);
                File.Copy(c, Path.Combine("contents/images", name[1]), true);
            }

            File.Copy(Application.streamingAssetsPath + "/appconfig.json", "appconfig.json", true);

            Invoke("QuitApp", 5);
        }
#endif
    }

    public static void LoadUIImage(string imageFileName, Image targetImage, Sprite preloadSprite = null)
    {
        string imagePath = Path.Combine("contents/images", imageFileName);

        if (preloadSprite == null)
        {
            if (File.Exists(imagePath))
            {
                // Carrega a textura da imagem
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                Texture2D tex = new Texture2D(2, 2); // Tamanho não importa aqui
                tex.LoadImage(imageBytes);

                // Atualiza a sprite da Image UI
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
                targetImage.sprite = sprite;
            }
            else
            {
                Debug.LogError("Arquivo não encontrado: " + imagePath);

                imagePath = Path.Combine("contents/images", "NoImageFound.png");

                byte[] imageBytes = File.ReadAllBytes(imagePath);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(imageBytes);

                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
                targetImage.sprite = sprite;
            }
        }
        else
        {
            targetImage.sprite = preloadSprite;
        }
    }

    public static void LoadSprite(string imageFileName, SpriteRenderer targetImage, Sprite preloadSprite = null)
    {
        string imagePath = Path.Combine("contents/images", imageFileName);

        if(preloadSprite == null)
        {
            if (File.Exists(imagePath))
            {
                // Carrega a textura da imagem
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                Texture2D tex = new Texture2D(2, 2); // Tamanho não importa aqui
                tex.LoadImage(imageBytes);

                // Atualiza a sprite da Image UI
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
                targetImage.sprite = sprite;
            }
            else
            {
                Debug.LogError("Arquivo não encontrado: " + imagePath);

                imagePath = Path.Combine("contents/images", "NoImageFound.png");

                byte[] imageBytes = File.ReadAllBytes(imagePath);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(imageBytes);

                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
                targetImage.sprite = sprite;
            }
        }
        else
        {
            targetImage.sprite = preloadSprite;
        }
    }

    public static IEnumerator LoadAudio(string mp3FileName, AudioSource audioSource, AudioClip preloadAudio = null)
    {
#if UNITY_EDITOR
        string projectPath = Application.dataPath.Replace("/Assets", "/contents");
#endif

#if !UNITY_EDITOR
        string[] splits = Application.dataPath.Split(Application.productName + "_Data");
        string projectPath = splits[0] + "/contents";
#endif

        // Crie o caminho para o arquivo .mp3 na pasta StreamingAssets
        string path = Path.Combine(projectPath + "/audios", mp3FileName);

        if (preloadAudio == null)
        {
            // Verifique se o arquivo existe
            if (File.Exists(path))
            {
                // Crie uma instância de UnityWebRequest
                UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG);

                // Aguarde o download
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogError("Erro ao carregar o arquivo de áudio: " + www.error);
                }
                else
                {
                    // Configure o AudioSource para reproduzir o áudio carregado
                    audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
                    audioSource.Play();
                }
            }
            else
            {
                Debug.LogError("Arquivo de áudio não encontrado: " + path);
            }
        }
        else
        {
            audioSource.clip = preloadAudio;
            audioSource.Play();
        }
    }

    public static void LoadUnityVideo(string videoFileName, VideoPlayer videoPlayer) //não usar
    {
#if UNITY_EDITOR
        string projectPath = Application.dataPath.Replace("/Assets", "/contents");
#endif

#if !UNITY_EDITOR
        string[] splits = Application.dataPath.Split(Application.productName + "_Data");
        string projectPath = splits[0] + "/contents";
#endif

        string path = Path.Combine(projectPath + "/videos", videoFileName);

        if (File.Exists(path))
        {
            // Configure o VideoPlayer para reproduzir o vídeo local
            videoPlayer.url = path;

            // Inicie a reprodução do vídeo
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("Arquivo de vídeo não encontrado: " + path);
        }
    }

    public static void LoadAVProVideo(string videoFileName, MediaPlayer MPOut, MediaReference preloadVideo = null, bool autoPlay = true)
    {
#if UNITY_EDITOR
        string projectPath = Application.dataPath.Replace("/Assets", "/contents");
#endif

#if !UNITY_EDITOR
        string[] splits = Application.dataPath.Split(Application.productName + "_Data");
        string projectPath = splits[0] + "/contents";
#endif

        string path = Path.Combine(projectPath + "/videos", videoFileName);

        if(preloadVideo == null)
        {
            if (MPOut.MediaOpened)
            {
                MPOut.Stop();
            }

            MPOut.OpenMedia(MediaPathType.AbsolutePathOrURL, path, autoPlay);
            var hints = MPOut.FallbackMediaHints;
            hints.stereoPacking = StereoPacking.CustomUV;
            MPOut.FallbackMediaHints = hints;
        }
        else
        {
            if (MPOut.MediaOpened)
            {
                MPOut.Stop();
            }

            MPOut.OpenMedia(preloadVideo, autoPlay);
            MPOut.Play();
            var hints = MPOut.FallbackMediaHints;
            hints.stereoPacking = StereoPacking.CustomUV;
            MPOut.FallbackMediaHints = hints;
        }
    }

    public static void LoadMaterial(string imageFileName, Renderer targetRenderer, Material preloadMaterial = null)
    {
        string imageFilePath = Path.Combine("contents/images", imageFileName);

        if (File.Exists(imageFilePath))
        {
            // Carrega a textura da imagem
            Texture2D texture = new Texture2D(2, 2);
            byte[] imageData = File.ReadAllBytes(imageFilePath);
            texture.LoadImage(imageData);

            // Cria um novo material com a textura
            Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            material.mainTexture = texture;

            // Aplica o material ao objeto 3D
            targetRenderer.material = material;
        }
        else if(preloadMaterial != null)
        {
            targetRenderer.material = preloadMaterial;
        }
        else
        {
            Debug.LogError("A imagem não foi encontrada em StreamingAssets: " + imageFilePath);

            imageFilePath = Path.Combine("contents/images", "NoImageFound.png");

            Texture2D texture = new Texture2D(2, 2);
            byte[] imageData = File.ReadAllBytes(imageFilePath);
            texture.LoadImage(imageData);

            Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            material.mainTexture = texture;

            targetRenderer.material = material;
        }
    }

    public static void VideoNotFound(MediaPlayer MPOut, bool autoPlay = true)
    {
#if UNITY_EDITOR
        string projectPath = Application.dataPath.Replace("/Assets", "/contents");
#endif

#if !UNITY_EDITOR
        string[] splits = Application.dataPath.Split(Application.productName + "_Data");
        string projectPath = splits[0] + "/contents";
#endif

        string path = Path.Combine(projectPath + "/videos", "NoVideoFound.mp4");

        if (MPOut.MediaOpened)
        {
            MPOut.Stop();
        }

        MPOut.OpenMedia(MediaPathType.AbsolutePathOrURL, path, autoPlay);
        var hints = MPOut.FallbackMediaHints;
        hints.stereoPacking = StereoPacking.CustomUV;
        MPOut.FallbackMediaHints = hints;
    }

    public static IEnumerator AudioNotFound(AudioSource audioSource)
    {
#if UNITY_EDITOR
        string projectPath = Application.dataPath.Replace("/Assets", "/contents");
#endif

#if !UNITY_EDITOR
        string[] splits = Application.dataPath.Split(Application.productName + "_Data");
        string projectPath = splits[0] + "/contents";
#endif

        string path = Path.Combine(projectPath + "/audios", "NoAudioFound.mp3");

        if (File.Exists(path))
        {
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Erro ao carregar o arquivo de áudio: " + www.error);
            }
            else
            {
                audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogError("Arquivo de áudio não encontrado: " + path);
        }
    }

    void QuitApp()
    {
        Debug.Log("Quit invoked");
        Application.Quit();
    }
}
