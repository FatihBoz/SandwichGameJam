using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health HealthInstance { get; private set; }

    private static int health = 100;

    public TextManager textmanager;

    private void Awake()
    {
        // Singleton kontrolü
        if (HealthInstance == null)
        {
            HealthInstance = this; // Singleton örneðini oluþtur
        }
        else if (HealthInstance != this)
        {
            Destroy(gameObject); // Eðer baþka bir Health örneði varsa yok et
        }
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (textmanager != null)
        {
            textmanager.ShowDamageText(damage);
        }
        else
        {
            Debug.LogWarning("TextManager atanmamýþ!");
        }

        Debug.Log("Kalan Saðlýk: " + health);
    }
}
