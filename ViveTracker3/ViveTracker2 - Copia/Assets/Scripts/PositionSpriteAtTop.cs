using UnityEngine;

public class PositionSpriteAtTop : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    public float offset;

    void Start()
    {
        // Obt�m a refer�ncia � c�mera principal
        mainCamera = Camera.main;
        // Obt�m a refer�ncia ao SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Posiciona o sprite no topo da tela
        PositionAtTop();
    }

    void PositionAtTop()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer n�o encontrado no GameObject.");
            return;
        }

        // Calcula a altura da c�mera
        float cameraHeight = 2f * mainCamera.orthographicSize;

        // Obt�m a altura do sprite
        float spriteHeight = spriteRenderer.bounds.size.y;

        // Calcula a nova posi��o do sprite
        float newY = mainCamera.transform.position.y + (cameraHeight / 2) - (spriteHeight / 2) - offset;
        float newX = mainCamera.transform.position.x;

        // Define a nova posi��o do sprite
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}