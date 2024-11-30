using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public Tower[] towerPrefabs;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlacementUI.Instance.Reset();
            // select mada faka on tower ui
            PlacementUI.Instance.SetPlacementLocation(this);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlacementUI.Instance.Reset();
        }
    }
}
