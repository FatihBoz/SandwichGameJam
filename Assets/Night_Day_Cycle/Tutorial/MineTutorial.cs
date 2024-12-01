using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTutorial : TutorialBase
{
    PlayerPurchase playerPurchase;
    protected override bool CheckTutorialCompletion()
    {
        return playerPurchase.GetCurrentGold()>120;
    }

    protected override void SetupTutorial()
    {
    }

    private void Start() {
            playerPurchase = FindObjectOfType<PlayerPurchase>();
        
    }
      private void Update()
    {
        if (isActive && CheckTutorialCompletion())
        {
            CompleteTutorial();
        }
}
}
