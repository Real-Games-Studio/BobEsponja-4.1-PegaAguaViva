using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class YDreamsUIImage : MonoBehaviour
{
    [SerializeField] Sprite backupSprite;

    private void Start()
    {
        if (File.Exists(Path.Combine("contents/images", gameObject.name + ".png")))
        {
            ProjectSetup.LoadUIImage(gameObject.name + ".png", GetComponent<Image>());
        }
        else if (backupSprite != null)
        {
            ProjectSetup.LoadUIImage(gameObject.name + ".png", GetComponent<Image>(), backupSprite);
        }
        else
        {
            ProjectSetup.LoadUIImage("ReturnError.png", GetComponent<Image>());
        }
    }
}
