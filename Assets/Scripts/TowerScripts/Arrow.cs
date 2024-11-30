using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifetime = 5f; // Okun yaþam süresi (saniye cinsinden)

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IPlayerCombat>(out var player))
        {
            player.TakeDamage(15);
        }
    }


}
