using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPointerTutorial : TutorialBase
{
    [SerializeField] private float requiredMovementDistance;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform mainTowerTransform;
    private PlayerMovement playerMovement;

    protected override bool CheckTutorialCompletion()
    {
        float distanceMoved = Vector3.Distance(mainTowerTransform.position, playerMovement.transform.position);
        return distanceMoved >= requiredMovementDistance;
    }

    protected override void SetupTutorial()
    {
        playerMovement=FindObjectOfType<PlayerMovement>();
        
    }

        private void Update()
    {
        if (isActive && CheckTutorialCompletion())
        {
            CompleteTutorial();
        }
    }
}
