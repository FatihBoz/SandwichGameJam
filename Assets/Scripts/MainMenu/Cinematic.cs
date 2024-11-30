using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Cinematic : MonoBehaviour
{
    [SerializeField] private List<PageSO> pages;
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] private GameObject darkScreen;
    [SerializeField] private float timeBetweenPageDisplays = 1.5f;

    private int listCounter = 0; 

    private void Start()
    {
        darkScreen.SetActive(false);
        UpdatePage();
    }

    public void OnImageClicked()
    {
        if (listCounter < pages.Count)
        {
            StartCoroutine(SwitchPageRoutine());
        }
    }


    private IEnumerator SwitchPageRoutine()
    {
        darkScreen.SetActive(true);
        darkScreen.GetComponent<Image>().DOFade(1, 0.5f);

        yield return new WaitForSeconds(0.5f);

        UpdatePage();

        yield return new WaitForSeconds(0.5f);

        darkScreen.GetComponent<Image>().DOFade(0, 0.5f).onComplete = () =>
        {
            darkScreen.SetActive(false);
        };

        yield return new WaitForSeconds(0.5f);
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
}
