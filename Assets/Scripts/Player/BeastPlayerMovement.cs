using System.Collections;
using UnityEngine;

public class BeastPlayerMovement : PlayerMovement, IPlayerMovement
{
    [SerializeField] private float dodgeDistance = 10f;
    [SerializeField] private float dodgeTime = .3f;
    [SerializeField] private float timeBetweenDodges = 1f;
    private bool isDodging;
    private float dodgeStartTime;
    private Vector3 dodgeDirection;
    private bool canDodge = true;

    private float lastDodgeTime = -Mathf.Infinity; // Infinity baþlangýçta sýnýrsýz bir bekleme saðlar.

    protected override void FixedUpdate()
    {
        // Oyuncunun zeminde olup olmadýðýný kontrol et.
        isGrounded = GroundCheck();

        // Eðer dodge iþlemi aktifse PerformDodge çaðrýlýr.
        if (isDodging)
        {
            PerformDodge();
        }

        // Zýplama girdisi alýnýr.
        if (InputReceiver.Instance.GetBeastPlayerJumpInput() == 1 && isGrounded)
        {
            Jump();
        }

        // Dodge giriþini kontrol et.
        if (InputReceiver.Instance.GetBeastPlayerDodgeInput() == 1
            && Time.time >= lastDodgeTime + timeBetweenDodges // Dodge bekleme süresi dolmuþ mu?
            && !isDodging) // Hali hazýrda dodge yapýlmýyor mu?
        {
            StartDodge();
        }

        // Hareket ve diðer iþlevleri çalýþtýr.
        Move();
        RotatePlayer();
        Animate();
    }

    private void StartDodge()
    {
        // Dodge iþlemini baþlat.
        isDodging = true;
        lastDodgeTime = Time.time; // Dodge baþlangýç zamanýný kaydet.
        dodgeStartTime = Time.time; // Dodge süresini baþlat.
        dodgeDirection = GetDodgeDirection(); // Dodge yönünü al.
        animator.SetBool("isDodging", true); // Animasyonu tetikle.
        print("Dodge baþladý.");
    }

    private void PerformDodge()
    {
        // Dodge iþlemi sürüyor mu?
        float elapsedTime = Time.time - dodgeStartTime;

        if (elapsedTime > dodgeTime)
        {
            EndDodge(); // Dodge iþlemini bitir.
        }
        else
        {
            // Karakter dodge yönüne doðru hareket eder.
            transform.position += dodgeDirection * Time.fixedDeltaTime / dodgeTime * dodgeDistance;
        }
    }

    private void EndDodge()
    {
        animator.SetBool("isDodging", false); // Animasyon durduruluyor.
        isDodging = false; // Dodge iþlemi tamamlandý.
        print("Dodge bitti.");
    }

    //private IEnumerator DodgeCooldown()
    //{
    //    yield return new WaitForSeconds(timeBetweenDodges);
    //    canDodge = true;
    //}


    protected override void Move()
    {
        float dir = InputReceiver.Instance.GetBeastPlayerMoveDirection();

        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
    }

    protected override void RotatePlayer()
    {
        if (rb.velocity.x > 0.1f)
        {
            transform.localScale = new(playerScale, playerScale);
        }
        else if (rb.velocity.x < -0.1f)
        {
            transform.localScale = new(-playerScale, playerScale);
        }
    }


    private Vector3 GetDodgeDirection()
    {
        Vector2 moveDirection = new Vector2(InputReceiver.Instance.GetBeastPlayerMoveDirection(),0);
        //if character does not move dash towards its own direction.Else dash according to movement.
        return !moveDirection.Equals(Vector2.zero) ? new Vector2(moveDirection.x, 0) : new Vector2(rb.velocity.x, 0).normalized;
    }

    public bool IsInvulnerable => isDodging;
}
