using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CycleManager : MonoBehaviour
{
    int dayCount = 1;
    int increaseDayCount = 0;

    private bool isMorning = true; // Sabah mý? Akþam mý?
    public RectTransform center; // Dönüþ merkezini UI elemaný olarak ayarla
    public RectTransform movingPoint; // Dönen ibre

    public float rotationDuration = 10f; // Dönüþ süresi (saniye)
    private float rotationSpeed; // Derece/saniye dönüþ hýzý
    private float currentRotation = 0f; // Ýbrenin mevcut rotasyonu

    // SpriteSwitcher objelerini bulmak için
    private ClockSpriteChanger[] spriteSwitchers;
    private SpriteChanger[] generalSpriteSwitchers;

    public TextMeshProUGUI dayText;

    void Start()
    {
        // Clock dönüþ hýzýný hesapla
        rotationSpeed = 360f / rotationDuration;

        // Tüm SpriteChanger bileþenlerini bul
        spriteSwitchers = FindObjectsOfType<ClockSpriteChanger>();

        UpdateSprites();
    }

    public void ToggleDayNight()
    {
        if(increaseDayCount == 1)
        {
            dayCount++;
            increaseDayCount = 0;
        }
        else
        {
            increaseDayCount++;

        }
        isMorning = !isMorning;
        dayText.text = $"{(isMorning ? "Morning" : "Night")} / Day : {dayCount}";
        UpdateSprites();
    }

    private void UpdateSprites()
    {
        generalSpriteSwitchers = FindObjectsOfType<SpriteChanger>();

        //Clocks changed
        foreach (ClockSpriteChanger switcher in spriteSwitchers)
        {
            if (isMorning)
            {
                switcher.SwitchToMorning();
            }
            else
            {
                switcher.SwitchToEvening();
            }
        }

        //Buildings Changed
        foreach (SpriteChanger switcher in generalSpriteSwitchers)
        {
            if (isMorning)
            {
                switcher.SwitchToMorning();
            }
            else
            {
                switcher.SwitchToEvening();
            }
        }
    }

    void Update()
    {
        // Ýbreyi döndür
        currentRotation += rotationSpeed * Time.deltaTime;
        movingPoint.localEulerAngles = new Vector3(0, 0, -currentRotation);

        // Tam dönüþ kontrolü (0 ile 360 arasýnda normalize et)
        if (currentRotation >= 360f)
        {
            currentRotation -= 360f;
            ToggleDayNight(); // Dönüþ tamamlanýnca gece-gündüz geçiþi
        }

        // Manuel sabah/akþam geçiþi için (Test için T tuþu)
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleDayNight();
        }
    }
}
