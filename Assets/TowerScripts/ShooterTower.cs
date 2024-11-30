using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShooterTower : MonoBehaviour
{
    public GameObject target; // Hedef objesi
    public TextManager textManager;
    public GameObject firePoint; // Ateþ noktasý

    public GameObject projectilePrefab; // Fýrlatýlacak obje


    [Header("Tower Options")]
    public float projectileSpeed = 0.1f; // Obje fýrlatma hýzý
    public float fireRate = 1f; // Ateþ etme sýklýðý
    public float detectionRange = 5f; // Menzil

    private float nextFireTime = 0f; // Ateþ etme zamaný


    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.transform.position);

        // Eðer mesafe menzil içindeyse ateþ et
        if (distanceToPlayer <= detectionRange)
        {
            if (Time.time >= nextFireTime)
            {
                FireAtTarget();
                nextFireTime = Time.time + fireRate; // Bir sonraki ateþ etme zamanýný belirle
            }
        }
    }

    private void FireAtTarget()
    {
        if (target == null || firePoint == null || projectilePrefab == null) return;

        // Ateþ noktasý pozisyonunda obje oluþtur
        GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

        // Hedefe doðru yönlendirme
        Vector2 direction = (target.transform.position - firePoint.transform.position).normalized;

        // Oku hedefe döndürme
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Yönü hesapla
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Oku döndür

        // Okun hýzýný ayarlama
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
    }


    private void OnDrawGizmos()
    {
        // Kule menzilini görselleþtirme
        Gizmos.color = Color.red; // Renk ayarý
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Menzil çemberi
    }

}
