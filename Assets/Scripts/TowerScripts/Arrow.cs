using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifetime = 5f; // Okun ya�am s�resi (saniye cinsinden)

    private void Start()
    {
        // Ok olu�turuldu�unda belirli bir s�re sonra kendisini yok et
        Destroy(gameObject, lifetime); // lifetime s�resi sonunda oku yok et
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IPlayerCombat>(out var player))
        {
            player.TakeDamage(15);
        }
    }
}
