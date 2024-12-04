using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTutorial : TutorialBase
{
    public static Action OnMineTutorialCompleted;
    PlayerPurchase playerPurchase;
    protected override bool CheckTutorialCompletion()
    {
        return playerPurchase.GetCurrentGold()>170;
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
            OnMineTutorialCompleted?.Invoke();
            CompleteTutorial();
        }
}
}
