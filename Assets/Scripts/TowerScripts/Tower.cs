using UnityEngine;


public class Tower : MonoBehaviour
{
    public float price;
    public Sprite towerIcon;
    public float maxHealth = 100f;  // Kule i�in maksimum can
    private float currentHealth;
    public GameObject myHealthBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        print("kule can�:" + currentHealth);
        if (currentHealth <= 0)
        {
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
}
