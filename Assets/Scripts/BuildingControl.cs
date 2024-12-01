using UnityEngine;

public class BuildingControl : MonoBehaviour
{
    [SerializeField] private Transform parentBuilding;


    private void BuildingControl_OnDayStarted()
    {
        for (int i = 0; i < parentBuilding.childCount; i++)
        {
            parentBuilding.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void BuildingControl_OnNightStarted()
    {
        for (int i = 0; i < parentBuilding.childCount; i++)
        {
            parentBuilding.GetChild(i).gameObject.SetActive(false);
        }
    }


    private void OnEnable()
    {
        CycleManager.OnNightStarted += BuildingControl_OnNightStarted;
        CycleManager.OnDayStarted += BuildingControl_OnDayStarted;
    }


    private void OnDisable()
    {
        CycleManager.OnNightStarted -= BuildingControl_OnNightStarted;
        CycleManager.OnDayStarted -= BuildingControl_OnDayStarted;
    }
}
