using UnityEngine;

public class ShooterTower : Tower
{
    public TextManager textManager;
    public GameObject firePoint;

    public GameObject projectilePrefab;


    [Header("Tower Options")]
    public float projectileSpeed = 0.1f; // Obje f�rlatma h�z�
    public float fireRate = 1f; // Ate� etme s�kl���
    public float detectionRange = 5f; // Menzil

    private float nextFireTime = 0f; // Ate� etme zaman�

    private GameObject target;
    [SerializeField] private float detectRadius = 7.5f;
    [SerializeField] private LayerMask beastLayer;

    private void Update()
    {
        if (target == null)
        {
            Collider2D enemy = Physics2D.OverlapCircle(transform.position, detectRadius, beastLayer);
            target = enemy.transform.gameObject;
            return;
        }

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
