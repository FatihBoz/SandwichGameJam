using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damageAmount = 15f; // Kuleye verecek hasar
    [SerializeField] private float stunTime = 2f; // Kuleye verecek hasar

    [SerializeField] private LayerMask towerLayer; // Sadece kulelere çarpsýn



    private void Start()
    {
        // Fýrlatma yönünü hesapla ve baþlat
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float direction = transform.localScale.x < 0 ? -1f : 1f; // Eðer karakter saða bakýyorsa, -1, sola bakýyorsa 1
        rb.velocity = new Vector2(direction * speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Tower>(out var tower))
        {
            print("buraya giriyor");
            tower.TakeDamage(15);
            tower.StopAttack(stunTime);
            // Projectile'i yok et
            Destroy(gameObject);
        }
    }


    private void Projectile_OnDayStarted()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        CycleManager.OnDayStarted += Projectile_OnDayStarted;
    }

    private void OnDisable()
    {
        CycleManager.OnDayStarted -= Projectile_OnDayStarted;
    }
}