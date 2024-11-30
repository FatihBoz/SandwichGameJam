using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerImage : MonoBehaviour
{
    private Tower tower;
    public void SetImageIcon(Sprite sprite)
    {
        GetComponent<Image>().sprite=sprite;
    }
    public void SetTower(Tower tower)
    {
        this.tower=tower;
    }
    public Tower GetTower()
    {
        return tower;
    }
}
