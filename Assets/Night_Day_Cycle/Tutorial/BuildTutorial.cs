using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTutorial : TutorialBase
{
    [SerializeField] private GameObject CheckActivenessGO;
    protected override bool CheckTutorialCompletion()
    {
        return CheckActivenessGO.activeInHierarchy;
    }
    protected override void SetupTutorial()
    {
        
    }
    private void Update()
    {
        if (isActive && CheckTutorialCompletion())
        {
            CompleteTutorial();
        }
    }

}
