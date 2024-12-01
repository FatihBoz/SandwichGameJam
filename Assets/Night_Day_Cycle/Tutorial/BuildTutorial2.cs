using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTutorial2 : TutorialBase
{
    private PlayerPurchase playerPurchase;
    private PlayerMovement playerMovement;

    protected override bool CheckTutorialCompletion()
    {
        return playerPurchase.GetCurrentGold() < 100;
    }

    protected override void SetupTutorial()
    {
        playerPurchase=FindObjectOfType<PlayerPurchase>();
        playerMovement=FindObjectOfType<PlayerMovement>();
    }

    public override void StartTutorial()
    {
        base.StartTutorial();
        playerMovement.SetIsStopped(true);
    }
       private void Update()
    {
        if (isActive && CheckTutorialCompletion())
        {
            CompleteTutorial();
        }
    }
    protected override void CompleteTutorial()
    {
        playerMovement.SetIsStopped(false);
        base.CompleteTutorial();
    }
}
