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
    public float fadeDuration = 1f; // Siyahla�ma s�resi
    public TextMeshProUGUI CutsceneText;

    [Header("Character Options")]
    public Transform Beast;
    public Transform Human;

    public Transform humanSpawn;
    public Transform beastSpawn;

    public GameObject BeastPanel;
    public GameObject HumanPanel;

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

    public static Action OnNightStarted;
    public static Action OnDayStarted;


    private bool isStopped;

    void Start()
    {
        Human.position = humanSpawn.position;

        Human.gameObject.SetActive(true);
        Beast.gameObject.SetActive(false);

        // Clock d�n�� h�z�n� hesapla
        rotationSpeed = 360f / rotationDuration;

        // T�m SpriteChanger bile�enlerini bul
        spriteSwitchers = FindObjectsOfType<ClockSpriteChanger>();

        UpdateSprites();
    }

    private IEnumerator FadeAndReload()
    {
        CutsceneText.text = $"{(isMorning ? "Morning" : "Night")} / Day {dayCount}";

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
        //CutsceneText.gameObject.SetActive(true);

        if (isMorning)
        {
            OnDayStarted?.Invoke();
            CameraControl.GetComponent<CameraControl>().SetTarget(Human);
            Human.position = humanSpawn.position;
            Human.gameObject.SetActive(true);
            Beast.gameObject.SetActive(false);

            HumanPanel.SetActive(true);
            BeastPanel.SetActive(false);

            Human.gameObject.GetComponent<PlayerPurchase>().AddGold(100);
        }
        else
        {
            OnNightStarted?.Invoke();
            CameraControl.GetComponent<CameraControl>().SetTarget(Beast);

            Beast.position = beastSpawn.position;
            Beast.gameObject.SetActive(true);
            Human.gameObject.SetActive(false);

            HumanPanel.SetActive(false);
            BeastPanel.SetActive(true);
        }


        dayText.text = $"{(isMorning ? "Morning" : "Night")} / Day {dayCount}";
        UpdateSprites();

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

        StartCoroutine(FadeAndReload());


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
        if (isStopped)
        {
            return;
        }

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
    public void SetIsStopped(bool isStopped)
    {
        this.isStopped=isStopped;
    }
}
