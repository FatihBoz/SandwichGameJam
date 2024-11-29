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
        // E�er �arp��an obje player ise
        if (other.CompareTag("player"))
        {
            // Player'a �arp�nca oku yok et
            Destroy(gameObject);
        }
    }
}
