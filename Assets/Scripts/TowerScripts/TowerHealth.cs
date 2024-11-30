using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Kule için maksimum can
    private float currentHealth;

    void Start()
    {
        // Kule baþlangýçta tam cana sahip olacak
        currentHealth = maxHealth;
    }

    // Kuleye hasar verme fonksiyonu
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;  // Hasar verilir
        if (currentHealth <= 0)
        {
            DestroyTower();  // Can sýfýrlanýrsa kule yok edilir
        }
    }

    // Kule yok edilince yapýlacak iþlemler
    void DestroyTower()
    {
        Debug.Log(gameObject.name + " kule yok oldu!");
        Destroy(gameObject);  // Kuleyi yok et
    }

    // Kule canýný al
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
