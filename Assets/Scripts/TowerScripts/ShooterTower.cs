using UnityEngine;

public class ShooterTower : Tower
{
    public Transform firePointRight;
    public Transform firePointLeft;

    public GameObject projectilePrefab;


    [Header("Tower Options")]
    public float projectileSpeed = 0.1f;
    public float fireRate = 1f;
    public float detectionRange = 5f;

    private float nextFireTime = 0f;

    private GameObject target;
    [SerializeField] private float detectRadius = 7.5f;
    [SerializeField] private LayerMask beastLayer;

    private Transform firePoint;

    private void Update()
    {
        if (target == null)
        {
            Collider2D enemy = Physics2D.OverlapCircle(transform.position, detectRadius, beastLayer);
            if (enemy != null)
            {
                target = enemy.transform.gameObject;
            }

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
        if (target == null || projectilePrefab == null) return;

        GameObject projectile;

        //Right skull is closer
        if (Vector2.Distance(target.transform.position, firePointRight.position) < Vector2.Distance(target.transform.position, firePointLeft.position))
        {
            firePoint = firePointRight;
            
        }
        else // if the distance is equal or left skull is closer
        {
            firePoint = firePointLeft;
        }

        projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

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


    private void ShooterTower_OnDayStarted()
    {
        target = null;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        CycleManager.OnDayStarted += ShooterTower_OnDayStarted;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CycleManager.OnDayStarted -= ShooterTower_OnDayStarted;
    }

}
