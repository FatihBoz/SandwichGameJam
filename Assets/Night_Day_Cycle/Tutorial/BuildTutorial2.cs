using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTutorial2 : TutorialBase
{
    [SerializeField]
    private PlayerPurchase playerPurchase;
    [SerializeField]
    private PlayerMovement playerMovement;
    private int currentGold;

    protected override bool CheckTutorialCompletion()
    {
        return playerPurchase.GetCurrentGold() < currentGold;
    }

    protected override void SetupTutorial()
    {
        playerMovement.SetIsStopped(true);
        currentGold=playerPurchase.GetCurrentGold();
    }
    private void Start() {
        playerPurchase=FindObjectOfType<PlayerPurchase>();
        playerMovement=FindObjectOfType<PlayerMovement>();
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
