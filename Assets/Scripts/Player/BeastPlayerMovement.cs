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

    private float lastDodgeTime = -Mathf.Infinity; // Infinity ba�lang��ta s�n�rs�z bir bekleme sa�lar.

    protected override void FixedUpdate()
    {
        // Oyuncunun zeminde olup olmad���n� kontrol et.
        isGrounded = GroundCheck();

        // E�er dodge i�lemi aktifse PerformDodge �a�r�l�r.
        if (isDodging)
        {
            PerformDodge();
        }

        // Z�plama girdisi al�n�r.
        if (InputReceiver.Instance.GetBeastPlayerJumpInput() == 1 && isGrounded)
        {
            Jump();
        }

        // Dodge giri�ini kontrol et.
        if (InputReceiver.Instance.GetBeastPlayerDodgeInput() == 1
            && Time.time >= lastDodgeTime + timeBetweenDodges // Dodge bekleme s�resi dolmu� mu?
            && !isDodging) // Hali haz�rda dodge yap�lm�yor mu?
        {
            StartDodge();
        }

        // Hareket ve di�er i�levleri �al��t�r.
        Move();
        RotatePlayer();
        Animate();
    }

    private void StartDodge()
    {
        // Dodge i�lemini ba�lat.
        isDodging = true;
        lastDodgeTime = Time.time; // Dodge ba�lang�� zaman�n� kaydet.
        dodgeStartTime = Time.time; // Dodge s�resini ba�lat.
        dodgeDirection = GetDodgeDirection(); // Dodge y�n�n� al.
        animator.SetBool("isDodging", true); // Animasyonu tetikle.
        print("Dodge ba�lad�.");
    }

    private void PerformDodge()
    {
        // Dodge i�lemi s�r�yor mu?
        float elapsedTime = Time.time - dodgeStartTime;

        if (elapsedTime > dodgeTime)
        {
            EndDodge(); // Dodge i�lemini bitir.
        }
        else
        {
            // Karakter dodge y�n�ne do�ru hareket eder.
            transform.position += dodgeDirection * Time.fixedDeltaTime / dodgeTime * dodgeDistance;
        }
    }

    private void EndDodge()
    {
        animator.SetBool("isDodging", false); // Animasyon durduruluyor.
        isDodging = false; // Dodge i�lemi tamamland�.
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
