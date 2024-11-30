using UnityEngine;
using UnityEngine.XR;

public class CycleManager : MonoBehaviour
{
    public bool isMorning = true;

    public Transform center;
    public Transform movingPoint;

    public float rotationDuration = 10f; // saniye CÝnsinden
    private float rotationSpeed;

    // Oyundaki tüm SpriteSwitcher objelerini bulmak için
    private SpriteChanger[] spriteSwitchers;

    void Start()
    {
        //Clock
        rotationSpeed = 360f / rotationDuration;


        spriteSwitchers = FindObjectsOfType<SpriteChanger>();
        UpdateSprites();
    }

    public void ToggleDayNight()
    {
        isMorning = !isMorning;
        UpdateSprites();
    }

    private void UpdateSprites()
    {
        foreach (SpriteChanger switcher in spriteSwitchers)
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
        movingPoint.RotateAround(center.position, Vector3.forward, -rotationSpeed * Time.deltaTime);

        // Ýbrenin dönüþünü kontrol et
        if (Mathf.Abs(movingPoint.localEulerAngles.z - 0f) < 0.1f)
        {
            // Eðer dönüþ tamamlandýysa, ToggleDayNight fonksiyonunu çaðýr
            ToggleDayNight();
        }

        if (Input.GetKeyDown(KeyCode.T)) // T'ye basýnca sabah/akþam deðiþir
        {
            ToggleDayNight();
        }
    }

}
