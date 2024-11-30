using UnityEngine;
using UnityEngine.UI;

public class TowerHealthBar : MonoBehaviour
{
    public GameObject towerObject;  // Kule saðlýðý referansý
    public Image healthBar;          // Yeþil saðlýk barý
    public Image redBackground;      // Kýrmýzý arka plan

    TowerHealth towerHealth;

    private void Awake()
    {
        towerHealth = towerObject.GetComponent<TowerHealth>();
    }

    void Start()
    {
        // Kule saðlýðýný almak
        if (towerHealth == null)
        {
            towerHealth = GetComponentInParent<TowerHealth>();  // Kule saðlýðýna referans ver
        }

        // Can barlarýný baþlangýç durumlarýna göre ayarla
        UpdateHealthBar();
    }

    void Update()
    {
        // Kule saðlýðý deðiþtikçe can barýný güncelle
        UpdateHealthBar();

    }

    private void UpdateHealthBar()
    {
        if (towerHealth != null)
        {
            // Kule saðlýðýnýn yüzde deðeri
            float healthPercentage = towerHealth.GetCurrentHealth() / towerHealth.maxHealth;

            // Yeþil saðlýk barýný güncelle
            healthBar.fillAmount = healthPercentage;

            // Kýrmýzý arka planý güncelle
            redBackground.fillAmount = 1 - healthPercentage;
        }
    }
}
