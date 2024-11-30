using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Tower : MonoBehaviour
{
    public float price;
    public Sprite towerIcon;
    public float maxHealth = 100f;  // Kule i�in maksimum can
    private float currentHealth;

    void Start()
    {
        // Kule ba�lang��ta tam cana sahip olacak
        currentHealth = maxHealth;
    }

    // Kuleye hasar verme fonksiyonu
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;  // Hasar verilir
        print("kule can�:" + currentHealth);
        if (currentHealth <= 0)
        {
            DestroyTower();  // Can s�f�rlan�rsa kule yok edilir
        }
    }

    // Kule yok edilince yap�lacak i�lemler
    void DestroyTower()
    {
        Debug.Log(gameObject.name + " kule yok oldu!");
        Destroy(gameObject);  // Kuleyi yok et
    }

    // Kule can�n� al
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
