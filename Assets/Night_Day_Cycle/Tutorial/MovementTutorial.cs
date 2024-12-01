using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : TutorialBase
{

    private PlayerMovement playerMovement;

    private Vector3 startPos;

    public float requiredMovementDistance=5f;
    protected override bool CheckTutorialCompletion()
    {
        float distanceMoved = Vector3.Distance(startPos, playerMovement.transform.position);
        return distanceMoved >= requiredMovementDistance;
    }

    protected override void SetupTutorial()
    {
        startPos=playerMovement.transform.position;

        
    }
    private void Start() {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        if (isActive && CheckTutorialCompletion())
        {
            CompleteTutorial();
        }
    }
}
