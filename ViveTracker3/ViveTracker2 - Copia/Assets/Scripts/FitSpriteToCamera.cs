using UnityEngine;

public class FitSpriteToCamera : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obt�m a refer�ncia � c�mera principal
        mainCamera = Camera.main;
        // Obt�m a refer�ncia ao SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ajusta o tamanho do sprite para caber na vis�o da c�mera
        FitToCamera();
        // Centraliza o sprite na c�mera
        CenterToCamera();
    }

    void FitToCamera()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer n�o encontrado no GameObject.");
            return;
        }

        // Calcula a altura e a largura da c�mera
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Obt�m o tamanho do sprite
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
        // Define a posi��o do GameObject para a posi��o da c�mera
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, transform.position.z);
    }
}