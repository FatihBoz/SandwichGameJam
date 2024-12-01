using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public List<GameObject> objToBeActivated;
    [SerializeField] private Image darkScreen;

    bool canClick = true;

    private IEnumerator DelayedSceneChange()
    {
        darkScreen.gameObject.SetActive(true);
        darkScreen.DOFade(1f, 0.75f);
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene("Level1");
    }

    public void OnStartButtonClicked()
    {
        if (canClick)
        {
            StartCoroutine(DelayedSceneChange());
            canClick = false;
        }
        
    }
}
