using UnityEngine;
using UnityEngine.UI;

public class ClockSpriteChanger : MonoBehaviour
{
    public Sprite morningSprite; // Sabah için kullanýlacak sprite
    public Sprite eveningSprite; // Akþam için kullanýlacak sprite

    private Image imageComponent;

    void Awake()
    {
        // Bu objede Image bileþenini al
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image bileþeni bu objede bulunamadý!");
        }
    }

    public void SwitchToMorning()
    {
        // Sabah sprite'ýný ata
        if (imageComponent != null && morningSprite != null)
        {
            imageComponent.sprite = morningSprite;
        }
    }

    public void SwitchToEvening()
    {
        // Akþam sprite'ýný ata
        if (imageComponent != null && eveningSprite != null)
        {
            imageComponent.sprite = eveningSprite;
        }
    }
}
