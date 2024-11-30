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
        if (other.TryGetComponent<IPlayerCombat>(out var player))
        {
            player.TakeDamage(15);
        }
    }
}
