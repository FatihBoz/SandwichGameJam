using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite morningSprite; // Sabah i�in kullan�lacak sprite
    public Sprite eveningSprite; // Ak�am i�in kullan�lacak sprite

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer bu objede bulunamad�!");
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
