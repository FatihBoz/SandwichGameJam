using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damageAmount = 15f; // Kuleye verecek hasar
    [SerializeField] private float stunTime = 2f; // Kuleye verecek hasar

    [SerializeField] private LayerMask towerLayer; // Sadece kulelere �arps�n



    private void Start()
    {
        // F�rlatma y�n�n� hesapla ve ba�lat
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float direction = transform.localScale.x < 0 ? -1f : 1f; // E�er karakter sa�a bak�yorsa, -1, sola bak�yorsa 1
        rb.velocity = new Vector2(direction * speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // E�er �arpan �ey bir kule ise
        if (collision.gameObject.layer == 6)
        {
            if (collision.gameObject.TryGetComponent<ShooterTower>(out var shooterTower))
            {
                shooterTower.StopAttackingTemporarily(stunTime); // 2 saniye boyunca atak yapmas�n� engelle
            }
            collision.gameObject.GetComponent<Tower>().TakeDamage(damageAmount);

            // Projectile'i yok et
            Destroy(gameObject);
        }
    }
}