using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Cinematic : MonoBehaviour
{
    [SerializeField] private List<PageSO> pages;
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] private Image darkScreen;
    [SerializeField] private float waitingTime = .75f;
    [SerializeField] private GameObject subtitleBackground;

    [SerializeField] private MainMenuManager mainMenuManager;

    private int listCounter = 0;
    float elapsedTime = 0f;
    float timeBetweenTransitions = 2f;

    bool canStart;


    private void Start()
    {
        darkScreen.DOFade(0f, waitingTime).OnComplete(OnFirstDarkScreenVanished);
        UpdatePage();
    }


    private void Update()
    {
        if (!canStart)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if (listCounter >= pages.Count)
        {
            StartCoroutine(DelayedSceneChange());
            canStart = false;
            return;
        }

         if (elapsedTime > timeBetweenTransitions)
        {
            StartCoroutine (SwitchPageRoutine());
            elapsedTime = 0f;
        }

        
    }


    private IEnumerator SwitchPageRoutine()
    {
        canStart = false;
        darkScreen.gameObject.SetActive(true);
        darkScreen.DOFade(1, waitingTime);
        subtitle.DOFade(0, waitingTime);
        yield return new WaitForSeconds(waitingTime);

        UpdatePage();

        yield return new WaitForSeconds(waitingTime);

        subtitle.DOFade(1f,waitingTime);
        darkScreen.DOFade(0, waitingTime);


        yield return new WaitForSeconds(waitingTime);
        canStart = true;
    }


    private void UpdatePage()
    {
        if (listCounter < pages.Count)
        {
            img.sprite = pages[listCounter].Sprite;
            subtitle.text = pages[listCounter].Subtitle;
            listCounter++;
        }
    }

    void OnFirstDarkScreenVanished()
    {
        canStart = true;
    }

    private IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(timeBetweenTransitions);
        
        foreach (GameObject obj in mainMenuManager.objToBeActivated)
        {
            obj.SetActive(false);
        }
    }
}
