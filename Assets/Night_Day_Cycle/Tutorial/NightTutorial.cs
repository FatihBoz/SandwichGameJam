using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTutorial : TutorialBase
{
    private MainTower mainTower;
    protected override bool CheckTutorialCompletion()
    {
        Tower[] towerList = FindObjectsByType<Tower>(FindObjectsSortMode.InstanceID);
        foreach (var item in towerList)
        {
            if (item.GetInstanceID()!=mainTower.GetInstanceID())
            {
                return false;
            }
        }
        return true;
    }
    private void Update()
    {
        if (isActive && CheckTutorialCompletion())
        {
            CompleteTutorial();
        }
    }
    protected override void SetupTutorial()
    {
       mainTower = FindAnyObjectByType<MainTower>();
        
    }
}
