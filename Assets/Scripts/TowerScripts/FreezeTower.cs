using UnityEngine;

public class HellTower : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float freezeTime = 3f;

    private bool isPlayerInRange = false;
    private float timer = 0f;

    void Update()
    {
        // Mesafe kontrolü
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
            timer = 0f; // Mesafeden çýkarsa sayacý sýfýrla
        }

        // Geri sayým
        if (isPlayerInRange)
        {
            timer += Time.deltaTime;
            //transform.LookAt(player); // Kule oyuncuya bakýyor

            if (timer >= freezeTime)
            {
                FreezePlayer();
                timer = 0f; // Tekrar çalýþtýrmak için sýfýrla
            }
        }
    }

    void FreezePlayer()
    {
        // Oyuncunun hareketini dondur
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void OnDrawGizmos()
    {
        // Kule menzilini görselleþtirme
        Gizmos.color = Color.blue; // Renk ayarý
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Menzil çemberi
    }
}