using UnityEngine;

public class RadiusCircle : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float radius;

    private void LateUpdate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            // Obtém o tamanho do sprite em unidades do mundo
            float spriteWidth = spriteRenderer.bounds.size.x;
            float spriteHeight = spriteRenderer.bounds.size.y;

            // Assume que o sprite é circular, então o raio é metade da largura ou altura
            radius = Mathf.Min(spriteWidth, spriteHeight) / 2f;
        }
    }
}