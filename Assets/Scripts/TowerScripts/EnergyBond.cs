using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EnergyBond : MonoBehaviour
{
    public Transform TowerFirePoint;

    #region READONLY
    private readonly int segmentCount = 20;
    private readonly float waveAmplitude = 0.5f;
    private readonly float waveFrequency = 2f;
    private readonly float waveSpeed = 2f;
    private readonly float detectionRange = 15f;
    private readonly float freezeDelay = 3f;
    private readonly float freezeDuration = 3f;
    #endregion READONLY


    private LineRenderer lineRenderer;
    private float playerStayTime = 0f;
    private bool isPlayerFrozen = false;
    private float freezeTimer = 0f;


    private GameObject target;
    [SerializeField] private float detectRadius = 7.5f;
    [SerializeField] private LayerMask beastLayer;

    private Tower tower;

    void Start()
    {
        tower = GetComponent<Tower>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount;
        lineRenderer.enabled = false; // Baþlangýçta çizgi gizli
    }

    void Update()
    {

        if (!tower.canAttack)
        {
            print("energy bond");
            lineRenderer.enabled = false;
            return;
        }

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
            playerRb.gameObject.GetComponent<Animator>().SetBool("Freeze", true);
            playerRb.velocity = Vector2.zero;
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        PlayerMovement movement = target.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }
        //textManager.ShowStunnedText();
        Debug.Log("Player is frozen!");
    }

    void UnfreezePlayer()
    {
        isPlayerFrozen = false;

        Rigidbody2D playerRb = target.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.gameObject.GetComponent<Animator>().SetBool("Freeze", false);
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation; // Tüm kýsýtlamalarý kaldýr
        }

        PlayerMovement movement = target.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = true;
        }
    }

    private void EnergyBond_OnDayStarted()
    {
        target = null;
    }

    private void OnEnable()
    {
        CycleManager.OnDayStarted += EnergyBond_OnDayStarted;
    }

    private void OnDisable()
    {
        CycleManager.OnDayStarted -= EnergyBond_OnDayStarted;
    }
}
