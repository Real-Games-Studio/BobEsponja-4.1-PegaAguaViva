using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class YDreamsSprite : MonoBehaviour
{
    [SerializeField] Sprite backupSprite;

    private void Start()
    {
        if (File.Exists(Path.Combine("contents/images", gameObject.name + ".png")))
        {
            ProjectSetup.LoadSprite(gameObject.name + ".png", GetComponent<SpriteRenderer>());
        }
        else if (backupSprite != null)
        {
            ProjectSetup.LoadSprite(gameObject.name + ".png", GetComponent<SpriteRenderer>(), backupSprite);
        }
        else
        {
            ProjectSetup.LoadSprite("ReturnError.png", GetComponent<SpriteRenderer>());
        }
    }
}
