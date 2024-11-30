using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(LineRenderer))]
public class EnergyBond : MonoBehaviour
{
    public Transform TowerFirePoint;
    public TextManager textManager;

    [Header("Wave Options")]
    public int segmentCount = 20; // Çizgi segment sayýsý
    public float waveAmplitude = 0.5f; // Dalganýn yüksekliði
    public float waveFrequency = 2f; // Dalganýn sýklýðý
    public float waveSpeed = 2f; // Dalganýn hareket hýzý

    [Header("Freeze Options")]
    public float detectionRange = 10f; // Baðýn oluþacaðý maksimum mesafe
    public float freezeDelay = 3f; // Dondurucu etkinin tetikleneceði süre
    public float freezeDuration = 5f; // Oyuncunun donmuþ kalacaðý süre

    private LineRenderer lineRenderer;
    private float playerStayTime = 0f; // Oyuncunun menzilde geçirdiði süre
    private bool isPlayerFrozen = false; // Oyuncunun dondurulup dondurulmadýðý
    private float freezeTimer = 0f; // Donma süresi sayacý


    private GameObject target;
    [SerializeField] private float detectRadius = 7.5f;
    [SerializeField] private LayerMask beastLayer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount;
        lineRenderer.enabled = false; // Baþlangýçta çizgi gizli
    }

    void Update()
    {
        if (target == null)
        {
            Collider2D enemy = Physics2D.OverlapCircle(transform.position, detectRadius, beastLayer);
            if (enemy != null)
            {
                target = enemy.transform.gameObject;
            }
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToPlayer <= detectionRange && !isPlayerFrozen)
        {
            lineRenderer.enabled = true; // Çizgiyi aktif et
            DrawWavyLine();

            playerStayTime += Time.deltaTime;

            if (playerStayTime >= freezeDelay)
            {
                FreezePlayer(); // Dondurucu etkiyi uygula
            }
        }
        else
        {
            lineRenderer.enabled = false; // Çizgiyi gizle
            playerStayTime = 0f; // Süreyi sýfýrla
        }

        // Eðer oyuncu donmuþsa, donma süresini kontrol et
        if (isPlayerFrozen)
        {
            freezeTimer += Time.deltaTime;
            if (freezeTimer >= freezeDuration)
            {
                UnfreezePlayer(); // Oyuncuyu serbest býrak
            }
        }
    }

    void DrawWavyLine()
    {
        Vector3 startPoint = TowerFirePoint.transform.position;
        Vector3 endPoint = target.transform.position;
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
        freezeTimer = 0f; // Donma süresi sayaç sýfýrlanýr

        Rigidbody2D playerRb = target.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero;
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        PlayerMovement movement = target.GetComponent<PlayerMovement>();
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

        Rigidbody2D playerRb = target.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation; // Tüm kýsýtlamalarý kaldýr
        }

        PlayerMovement movement = target.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        // Kule menzilini görselleþtirme
        Gizmos.color = Color.blue; // Renk ayarý
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Menzil çemberi
    }
}
