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
        // Singleton kontrol�
        if (HealthInstance == null)
        {
            HealthInstance = this; // Singleton �rne�ini olu�tur
        }
        else if (HealthInstance != this)
        {
            Destroy(gameObject); // E�er ba�ka bir Health �rne�i varsa yok et
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
            Debug.LogWarning("TextManager atanmam��!");
        }

        Debug.Log("Kalan Sa�l�k: " + health);
    }
}
