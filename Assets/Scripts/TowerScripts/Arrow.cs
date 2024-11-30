using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifetime = 5f; // Okun yaþam süresi (saniye cinsinden)

    private void Start()
    {
        // Ok oluþturulduðunda belirli bir süre sonra kendisini yok et
        Destroy(gameObject, lifetime); // lifetime süresi sonunda oku yok et
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer çarpýþan obje player ise
        if (other.CompareTag("Player"))
        {
            Health.HealthInstance.GetDamage(15);
            Destroy(gameObject);
        }
    }
}
