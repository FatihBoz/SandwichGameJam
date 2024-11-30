using UnityEngine;
using UnityEngine.UI;

public class TowerHealthBar : MonoBehaviour
{
    public GameObject towerObject;  // Kule sa�l��� referans�
    public Slider healthBar;          // Ye�il sa�l�k bar�
    Tower tower;

    private GameObject canvasGO;
    public Vector3 offset;
    private void Awake()
    {
        tower = towerObject.GetComponent<Tower>();
    }

    void Start()
    {
        
        canvasGO = GameObject.Find("WorldSpaceCanvas");
        // Kule sa�l���n� almak
        if (tower == null)
        {
            tower = GetComponentInParent<Tower>();  // Kule sa�l���na referans ver
        }

        // Can barlar�n� ba�lang�� durumlar�na g�re ayarla
        UpdateHealthBar();


        if (canvasGO!=null)
        {
            transform.SetParent(canvasGO.transform);
           
        }
    }

    void Update()
    {
         transform.position=tower.transform.position+offset;
        // Kule sa�l��� de�i�tik�e can bar�n� g�ncelle
        UpdateHealthBar();

    }

    private void UpdateHealthBar()
    {
        if (tower != null)
        {
            // Kule sa�l���n�n y�zde de�eri
            float healthPercentage = tower.GetCurrentHealth() / tower.maxHealth;
            
            // Ye�il sa�l�k bar�n� g�ncelle
            healthBar.value = healthPercentage;

        }
    }
}
