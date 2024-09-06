using UnityEngine;

public class FitSpriteToCamera : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obtém a referência à câmera principal
        mainCamera = Camera.main;
        // Obtém a referência ao SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ajusta o tamanho do sprite para caber na visão da câmera
        FitToCamera();
        // Centraliza o sprite na câmera
        CenterToCamera();
    }

    void FitToCamera()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer não encontrado no GameObject.");
            return;
        }

        // Calcula a altura e a largura da câmera
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Obtém o tamanho do sprite
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        // Calcula a nova escala do sprite
        Vector3 newScale = transform.localScale;
        newScale.x = cameraWidth / spriteSize.x;
        newScale.y = cameraHeight / spriteSize.y;

        // Aplica a nova escala ao sprite
        transform.localScale = newScale;
    }

    void CenterToCamera()
    {
        // Define a posição do GameObject para a posição da câmera
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, transform.position.z);
    }
}