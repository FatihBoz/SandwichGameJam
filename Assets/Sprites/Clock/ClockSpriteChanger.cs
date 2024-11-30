using UnityEngine;
using UnityEngine.UI;

public class ClockSpriteChanger : MonoBehaviour
{
    public Sprite morningSprite; // Sabah i�in kullan�lacak sprite
    public Sprite eveningSprite; // Ak�am i�in kullan�lacak sprite

    private Image imageComponent;

    void Awake()
    {
        // Bu objede Image bile�enini al
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image bile�eni bu objede bulunamad�!");
        }
    }

    public void SwitchToMorning()
    {
        // Sabah sprite'�n� ata
        if (imageComponent != null && morningSprite != null)
        {
            imageComponent.sprite = morningSprite;
        }
    }

    public void SwitchToEvening()
    {
        // Ak�am sprite'�n� ata
        if (imageComponent != null && eveningSprite != null)
        {
            imageComponent.sprite = eveningSprite;
        }
    }
}
