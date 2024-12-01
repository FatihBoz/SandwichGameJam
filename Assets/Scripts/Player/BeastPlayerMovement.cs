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
        if (isStopped)
        {
            return;
        }
        isGrounded = GroundCheck();

        if (isDodging)
        {
            PerformDodge();
        }


        if (InputReceiver.Instance.GetBeastPlayerJumpInput() == 1 && isGrounded)
        {
            Jump();
        }


        if (InputReceiver.Instance.GetBeastPlayerDodgeInput() == 1
            && Time.time >= lastDodgeTime + timeBetweenDodges 
            && !isDodging)
        {
            StartDodge();
        }

 
        Move();
        RotatePlayer();
        Animate();
    }

    private void StartDodge()
    {
        isDodging = true;
        lastDodgeTime = Time.time;
        dodgeStartTime = Time.time;
        dodgeDirection = GetDodgeDirection();
        animator.SetBool("isDodging", true); 
    }

    private void PerformDodge()
    {
        float elapsedTime = Time.time - dodgeStartTime;

        if (elapsedTime > dodgeTime)
        {
            EndDodge();
        }
        else
        {
            transform.position += dodgeDirection * Time.fixedDeltaTime / dodgeTime * dodgeDistance;
        }
    }

    private void EndDodge()
    {
        animator.SetBool("isDodging", false);
        isDodging = false;
    }


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
        return !moveDirection.Equals(Vector2.zero) ? new Vector2(moveDirection.x, 0) : new Vector2(transform.localScale.x, 0).normalized;
    }

    public bool IsInvulnerable => isDodging;

}
