using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShooterTower : Tower
{
    public GameObject target; // Hedef objesi
    public TextManager textManager;
    public GameObject firePoint; // Ate� noktas�

    public GameObject projectilePrefab; // F�rlat�lacak obje


    [Header("Tower Options")]
    public float projectileSpeed = 0.1f; // Obje f�rlatma h�z�
    public float fireRate = 1f; // Ate� etme s�kl���
    public float detectionRange = 5f; // Menzil

    private float nextFireTime = 0f; // Ate� etme zaman�


    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.transform.position);

        // E�er mesafe menzil i�indeyse ate� et
        if (distanceToPlayer <= detectionRange)
        {
            if (Time.time >= nextFireTime)
            {
                FireAtTarget();
                nextFireTime = Time.time + fireRate; // Bir sonraki ate� etme zaman�n� belirle
            }
        }
    }

    private void FireAtTarget()
    {
        if (target == null || firePoint == null || projectilePrefab == null) return;

        // Ate� noktas� pozisyonunda obje olu�tur
        GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

        // Hedefe do�ru y�nlendirme
        Vector2 direction = (target.transform.position - firePoint.transform.position).normalized;

        // Oku hedefe d�nd�rme
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Y�n� hesapla
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Oku d�nd�r

        // Okun h�z�n� ayarlama
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
    }


    private void OnDrawGizmos()
    {
        // Kule menzilini g�rselle�tirme
        Gizmos.color = Color.red; // Renk ayar�
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Menzil �emberi
    }

}
