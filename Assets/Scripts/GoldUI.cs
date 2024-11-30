using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    private TMP_Text goldText;
    private PlayerPurchase playerPurchase;
    private void Start() {
        goldText=GetComponentInChildren<TMP_Text>();
        playerPurchase=FindAnyObjectByType<PlayerPurchase>();

    }
    private void Update() {
        if (playerPurchase!=null)
        {
            goldText.text="GOLD: "+playerPurchase.GetCurrentGold();            
        }
    }
}
