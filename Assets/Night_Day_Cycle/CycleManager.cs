using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CycleManager : MonoBehaviour
{
    int dayCount = 1;
    int increaseDayCount = 0;

    [Header("Camera Options")]
    public GameObject CameraControl;

    [Header("Fade Options")]
    public CanvasGroup blackScreenCanvasGroup; // Siyah ekran
    private bool isFading = false;
    public float fadeDuration = 1f; // Siyahlaþma süresi
    public TextMeshProUGUI CutsceneText;

    [Header("Character Options")]
    public Transform Beast;
    public Transform Human;

    private bool isMorning = true; // Sabah mý? Akþam mý?
    private bool isHuman = true;

    [Header("Clock Options")]
    public RectTransform center; // Dönüþ merkezini UI elemaný olarak ayarla
    public RectTransform movingPoint; // Dönen ibre
    public float rotationDuration = 10f; // Dönüþ süresi (saniye)
    private float rotationSpeed; // Derece/saniye dönüþ hýzý
    private float currentRotation = 0f; // Ýbrenin mevcut rotasyonu

    // SpriteSwitcher objelerini bulmak için
    private ClockSpriteChanger[] spriteSwitchers;
    private SpriteChanger[] generalSpriteSwitchers;

    public TextMeshProUGUI dayText;

    public static Action OnBuildingDisabled;

    void Start()
    {
        Human.gameObject.SetActive(true);
        Beast.gameObject.SetActive(false);

        // Clock dönüþ hýzýný hesapla
        rotationSpeed = 360f / rotationDuration;

        // Tüm SpriteChanger bileþenlerini bul
        spriteSwitchers = FindObjectsOfType<ClockSpriteChanger>();

        UpdateSprites();
    }

    private IEnumerator FadeAndReload()
    {
        CutsceneText.text = $"{(isMorning ? "Morning" : "Night")} / Day {dayCount}";

        isFading = true;

        // Oyun durduruluyor
        Time.timeScale = 0;

        // Ekraný siyahlaþtýr
        for (float t = 0; t <= fadeDuration; t += Time.unscaledDeltaTime)
        {
            blackScreenCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        blackScreenCanvasGroup.alpha = 1;

        // Mesajý göster
        //CutsceneText.gameObject.SetActive(true);

        // Kýsa bir süre bekle
        yield return new WaitForSecondsRealtime(2f);

        for (float t = fadeDuration; t >= 0; t -= Time.unscaledDeltaTime)
        {
            blackScreenCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        blackScreenCanvasGroup.alpha = 0;

        Time.timeScale = 1;
        isFading = false;
    }


    public void ToggleDayNight()
    {
        isMorning = !isMorning;
        if (increaseDayCount == 1)
        {

            dayCount++;
            increaseDayCount = 0;
        }
        else
        {
            increaseDayCount++;

        }

        Debug.Log("Aloo gün deðiþti alo");
        StartCoroutine(FadeAndReload());

        if (isMorning)
        {
            CameraControl.GetComponent<CameraControl>().SetTarget(Human);

            Human.gameObject.SetActive(true);
            Beast.gameObject.SetActive(false);
        }
        else
        {
            CameraControl.GetComponent<CameraControl>().SetTarget(Beast);
            Beast.gameObject.SetActive(true);
            Human.gameObject.SetActive(false);
        }

        if (!isMorning)
        {
            OnBuildingDisabled?.Invoke();
        }


        dayText.text = $"{(isMorning ? "Morning" : "Night")} / Day {dayCount}";
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
