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
        // Mesafe kontrol�
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
            timer = 0f; // Mesafeden ��karsa sayac� s�f�rla
        }

        // Geri say�m
        if (isPlayerInRange)
        {
            timer += Time.deltaTime;
            //transform.LookAt(player); // Kule oyuncuya bak�yor

            if (timer >= freezeTime)
            {
                FreezePlayer();
                timer = 0f; // Tekrar �al��t�rmak i�in s�f�rla
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
        /*
        // Alternatif olarak ba�ka bir hareket scriptini devre d��� b�rakabilirsiniz
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }
        */
    }

    private void OnDrawGizmos()
    {
        // Kule menzilini g�rselle�tirme
        Gizmos.color = Color.blue; // Renk ayar�
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Menzil �emberi
    }
}