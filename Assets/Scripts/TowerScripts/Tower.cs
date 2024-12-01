using System;
using UnityEngine;


public class Tower : MonoBehaviour
{
    public float price;
    public Sprite towerIcon;
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject myHealthBar;

    private GameObject placementTestPrefab;
    private float missingHealthPercentageToHeal = .5f;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        print("kule can�:" + currentHealth);
        if (currentHealth <= 0)
        {
            if (placementTestPrefab != null)
            {
                placementTestPrefab.SetActive(true);
            }
            Destroy(myHealthBar);
            DestroyTower();
        }
    }


    void DestroyTower()
    {
        Debug.Log(gameObject.name + " kule yok oldu!");
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetPlacement(GameObject placement)
    {
        placementTestPrefab = placement;
    }


    private void Tower_OnDayStarted()
    {
        currentHealth += (maxHealth - currentHealth) * missingHealthPercentageToHeal;
    }


    protected virtual void OnEnable()
    {
        CycleManager.OnDayStarted += Tower_OnDayStarted;
    }



    protected virtual void OnDisable()
    {
        CycleManager.OnDayStarted -= Tower_OnDayStarted;
    }
}
