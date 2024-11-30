using UnityEngine;
using UnityEngine.XR;

public class CycleManager : MonoBehaviour
{
    public bool isMorning = true;

    public Transform center;
    public Transform movingPoint;

    public float rotationDuration = 10f; // saniye C�nsinden
    private float rotationSpeed;

    // Oyundaki t�m SpriteSwitcher objelerini bulmak i�in
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

        // �brenin d�n���n� kontrol et
        if (Mathf.Abs(movingPoint.localEulerAngles.z - 0f) < 0.1f)
        {
            // E�er d�n�� tamamland�ysa, ToggleDayNight fonksiyonunu �a��r
            ToggleDayNight();
        }

        if (Input.GetKeyDown(KeyCode.T)) // T'ye bas�nca sabah/ak�am de�i�ir
        {
            ToggleDayNight();
        }
    }

}
