using UnityEngine;
using UnityEngine.UI;

public class TowerHealthBar : MonoBehaviour
{
    public GameObject towerObject;  // Kule sa�l��� referans�
    public Image healthBar;          // Ye�il sa�l�k bar�
    public Image redBackground;      // K�rm�z� arka plan

    TowerHealth towerHealth;

    private void Awake()
    {
        towerHealth = towerObject.GetComponent<TowerHealth>();
    }

    void Start()
    {
        // Kule sa�l���n� almak
        if (towerHealth == null)
        {
            towerHealth = GetComponentInParent<TowerHealth>();  // Kule sa�l���na referans ver
        }

        // Can barlar�n� ba�lang�� durumlar�na g�re ayarla
        UpdateHealthBar();
    }

    void Update()
    {
        // Kule sa�l��� de�i�tik�e can bar�n� g�ncelle
        UpdateHealthBar();

    }

    private void UpdateHealthBar()
    {
        if (towerHealth != null)
        {
            // Kule sa�l���n�n y�zde de�eri
            float healthPercentage = towerHealth.GetCurrentHealth() / towerHealth.maxHealth;

            // Ye�il sa�l�k bar�n� g�ncelle
            healthBar.fillAmount = healthPercentage;

            // K�rm�z� arka plan� g�ncelle
            redBackground.fillAmount = 1 - healthPercentage;
        }
    }
}
