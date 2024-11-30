using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPurchase : MonoBehaviour
{
    public int currentGold;

    public void AddGold(int amount)
    {
        currentGold+=amount;
    }

    public void DecreaseGold(int amount)
    {
        currentGold-=amount;
    }
    public int GetCurrentGold()
    {
        return currentGold;
    }
    
}
