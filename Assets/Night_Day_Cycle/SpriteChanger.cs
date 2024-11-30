using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite morningSprite; // Sabah için kullanýlacak sprite
    public Sprite eveningSprite; // Akþam için kullanýlacak sprite

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer bu objede bulunamadý!");
        }
    }

    public void SwitchToMorning()
    {
        if (spriteRenderer != null && morningSprite != null)
        {
            spriteRenderer.sprite = morningSprite;
        }
    }

    public void SwitchToEvening()
    {
        if (spriteRenderer != null && eveningSprite != null)
        {
            spriteRenderer.sprite = eveningSprite;
        }
    }
}
