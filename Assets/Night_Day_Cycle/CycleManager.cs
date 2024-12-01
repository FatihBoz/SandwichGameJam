using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CycleManager : MonoBehaviour
{
    int dayCount = 1;
    int increaseDayCount = 0;

    [Header("Fade Options")]
    public CanvasGroup blackScreenCanvasGroup; // Siyah ekran
    private bool isFading = false;
    public float fadeDuration = 1f; // Siyahla�ma s�resi
    public TextMeshProUGUI CutsceneText;

    private bool isMorning = true; // Sabah m�? Ak�am m�?

    [Header("Clock Options")]
    public RectTransform center; // D�n�� merkezini UI eleman� olarak ayarla
    public RectTransform movingPoint; // D�nen ibre
    public float rotationDuration = 10f; // D�n�� s�resi (saniye)
    private float rotationSpeed; // Derece/saniye d�n�� h�z�
    private float currentRotation = 0f; // �brenin mevcut rotasyonu

    // SpriteSwitcher objelerini bulmak i�in
    private ClockSpriteChanger[] spriteSwitchers;
    private SpriteChanger[] generalSpriteSwitchers;

    public TextMeshProUGUI dayText;

    public static Action OnBuildingDisabled;

    void Start()
    {
        // Clock d�n�� h�z�n� hesapla
        rotationSpeed = 360f / rotationDuration;

        // T�m SpriteChanger bile�enlerini bul
        spriteSwitchers = FindObjectsOfType<ClockSpriteChanger>();

        UpdateSprites();
    }

    private IEnumerator FadeAndReload()
    {
        Debug.Log("��erideyiz ");

        isFading = true;

        // Oyun durduruluyor
        Time.timeScale = 0;

        // Ekran� siyahla�t�r
        for (float t = 0; t <= fadeDuration; t += Time.unscaledDeltaTime)
        {
            blackScreenCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        blackScreenCanvasGroup.alpha = 1;

        // Mesaj� g�ster
        CutsceneText.text = $"{(isMorning ? "Morning" : "Night")} / Day : {dayCount}"; 
        //CutsceneText.gameObject.SetActive(true);

        // K�sa bir s�re bekle
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
        Debug.Log("Aloo g�n de�i�ti alo");
        StartCoroutine(FadeAndReload()); // Coroutine olarak ba�lat
        if (!isMorning)
        {
            OnBuildingDisabled?.Invoke();
        }

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
        // �breyi d�nd�r
        currentRotation += rotationSpeed * Time.deltaTime;
        movingPoint.localEulerAngles = new Vector3(0, 0, -currentRotation);

        // Tam d�n�� kontrol� (0 ile 360 aras�nda normalize et)
        if (currentRotation >= 360f)
        {
            currentRotation -= 360f;
            ToggleDayNight(); // D�n�� tamamlan�nca gece-g�nd�z ge�i�i
        }

        // Manuel sabah/ak�am ge�i�i i�in (Test i�in T tu�u)
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleDayNight();
        }
    }
}
