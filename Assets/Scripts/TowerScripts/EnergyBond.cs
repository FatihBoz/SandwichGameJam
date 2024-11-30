using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EnergyBond : MonoBehaviour
{
    public Transform player; // Oyuncu transformu
    public Transform TowerFirePoint;
    public TextManager textManager;

    [Header("Wave Options")]
    public int segmentCount = 20; // �izgi segment say�s�
    public float waveAmplitude = 0.5f; // Dalgan�n y�ksekli�i
    public float waveFrequency = 2f; // Dalgan�n s�kl���
    public float waveSpeed = 2f; // Dalgan�n hareket h�z�

    [Header("Freeze Options")]
    public float detectionRange = 10f; // Ba��n olu�aca�� maksimum mesafe
    public float freezeDelay = 3f; // Dondurucu etkinin tetiklenece�i s�re
    public float freezeDuration = 5f; // Oyuncunun donmu� kalaca�� s�re

    private LineRenderer lineRenderer;
    private float playerStayTime = 0f; // Oyuncunun menzilde ge�irdi�i s�re
    private bool isPlayerFrozen = false; // Oyuncunun dondurulup dondurulmad���
    private float freezeTimer = 0f; // Donma s�resi sayac�

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount;
        lineRenderer.enabled = false; // Ba�lang��ta �izgi gizli
    }

    void Update()
    {
        //Debug.Log("player stay time : " + playerStayTime);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && !isPlayerFrozen)
        {
            lineRenderer.enabled = true; // �izgiyi aktif et
            DrawWavyLine();

            playerStayTime += Time.deltaTime;

            if (playerStayTime >= freezeDelay)
            {
                FreezePlayer(); // Dondurucu etkiyi uygula
            }
        }
        else
        {
            lineRenderer.enabled = false; // �izgiyi gizle
            playerStayTime = 0f; // S�reyi s�f�rla
        }

        // E�er oyuncu donmu�sa, donma s�resini kontrol et
        if (isPlayerFrozen)
        {
            freezeTimer += Time.deltaTime;
            if (freezeTimer >= freezeDuration)
            {
                UnfreezePlayer(); // Oyuncuyu serbest b�rak
            }
        }
    }

    void DrawWavyLine()
    {
        Vector3 startPoint = TowerFirePoint.transform.position;
        Vector3 endPoint = player.position;
        Vector3 direction = (endPoint - startPoint) / (segmentCount - 1);

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 segmentPos = startPoint + direction * i;
            float offset = Mathf.Sin((i + Time.time * waveSpeed) * waveFrequency) * waveAmplitude;
            segmentPos.y += offset;
            lineRenderer.SetPosition(i, segmentPos);
        }
    }

    void FreezePlayer()
    {
        isPlayerFrozen = true;
        freezeTimer = 0f; // Donma s�resi saya� s�f�rlan�r

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero;
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }
        textManager.ShowStunnedText();
        Debug.Log("Player is frozen!");
    }

    void UnfreezePlayer()
    {
        isPlayerFrozen = false;

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation; // T�m k�s�tlamalar� kald�r
        }

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = true;
        }

        Debug.Log("Player is unfrozen!");
    }

    private void OnDrawGizmos()
    {
        // Kule menzilini g�rselle�tirme
        Gizmos.color = Color.blue; // Renk ayar�
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Menzil �emberi
    }
}
