using UnityEngine;

public class CycleManager : MonoBehaviour
{
    public bool isMorning = true; // Sabah mý? Akþam mý?
    public RectTransform center; // Dönüþ merkezini UI elemaný olarak ayarla
    public RectTransform movingPoint; // Dönen ibre

    public float rotationDuration = 10f; // Dönüþ süresi (saniye)
    private float rotationSpeed; // Derece/saniye dönüþ hýzý
    private float currentRotation = 0f; // Ýbrenin mevcut rotasyonu

    // SpriteSwitcher objelerini bulmak için
    private ClockSpriteChanger[] spriteSwitchers;
    private SpriteChanger[] generalSpriteSwitchers;


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
        isMorning = !isMorning;
        UpdateSprites();
    }

    private void UpdateSprites()
    {
        generalSpriteSwitchers = FindObjectsOfType<SpriteChanger>();


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
        foreach (SpriteChanger switcher in generalSpriteSwitchers)
        {
            Debug.Log("Aloooo" + switcher.gameObject.name);
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
            Debug.Log("Föngüüüüü");
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
