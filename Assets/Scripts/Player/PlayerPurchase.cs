using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPurchase : MonoBehaviour
{
    public int currentGold;
    private int tempGold;
    int maxGoldCanBeGained = 200;

    public void AddGold(int amount)
    {

        tempGold += amount;
        if (tempGold  < maxGoldCanBeGained)
        {
            currentGold += amount;
        }
    }

    public void DecreaseGold(int amount)
    {
        currentGold-=amount;
    }
    public int GetCurrentGold()
    {
        return currentGold;
    }

    private void OnEnable()
    {
        CycleManager.OnDayStarted += PlayerPurchase_DayStart;
    }

    private void PlayerPurchase_DayStart()
    {
        tempGold = 0;
    }

    private void OnDisable()
    {
        CycleManager.OnDayStarted -= PlayerPurchase_DayStart;
    }
}
