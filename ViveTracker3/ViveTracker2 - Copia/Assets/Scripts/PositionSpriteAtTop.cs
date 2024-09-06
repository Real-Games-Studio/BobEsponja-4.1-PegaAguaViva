using UnityEngine;

public class PositionSpriteAtTop : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    public float offset;

    void Start()
    {
        // Obtém a referência à câmera principal
        mainCamera = Camera.main;
        // Obtém a referência ao SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Posiciona o sprite no topo da tela
        PositionAtTop();
    }

    void PositionAtTop()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer não encontrado no GameObject.");
            return;
        }

        // Calcula a altura da câmera
        float cameraHeight = 2f * mainCamera.orthographicSize;

        // Obtém a altura do sprite
        float spriteHeight = spriteRenderer.bounds.size.y;

        // Calcula a nova posição do sprite
        float newY = mainCamera.transform.position.y + (cameraHeight / 2) - (spriteHeight / 2) - offset;
        float newX = mainCamera.transform.position.x;

        // Define a nova posição do sprite
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}