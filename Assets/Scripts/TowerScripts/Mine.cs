using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject GainGoldPanel;
    private GameObject PlayerGameObject;
    public bool gainable;

    private int goldAmount = 5;

  
     void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Giriyoooooo");

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("aaa player");

            PlayerGameObject = collision.gameObject;

            GainGoldPanel.SetActive(true);

            gainable = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Giriyoooooo");
        if (collision.gameObject.CompareTag("Player"))
        {
            GainGoldPanel.SetActive(false);
            gainable = false;
        }
    }

    private void Update()
    {
        if (gainable && Input.GetKeyDown(KeyCode.E))
        {
            PlayerGameObject.GetComponent<PlayerPurchase>().AddGold(goldAmount);
        }
    }
}
