using System;
using UnityEngine;

public class PlayerPurchase : MonoBehaviour
{
    public static PlayerPurchase Instance;
    public int currentGold;
    private int tempGold;
    int maxGoldCanBeGained = 100;

    private void Awake()
    {
        Instance = this;
    }

    public void AddGold(int amount)
    {
        if (tempGold  < maxGoldCanBeGained)
        {
            tempGold += amount;
            currentGold += amount;
        }
    }

    public void AddGoldForDayStart(int amount)
    {
        currentGold += amount;
    }

    public void DecreaseGold(int amount)
    {
        currentGold-=amount;
    }
    public int GetCurrentGold()
    {
        return currentGold;
    }

    public void ResetTempGold()
    {
        print("temp gold sýfýrlandý");
        tempGold = 0;
    }
}
