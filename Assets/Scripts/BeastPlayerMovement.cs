using UnityEngine;

public class BeastPlayerMovement : PlayerMovement
{
    [SerializeField] private float dodgeDistance = 10f;
    [SerializeField] private float dodgeTime = .3f;
    [SerializeField]
    private bool isDodging;
    private float dodgeStartTime;
    private Vector3 dodgeDirection;


    protected override void FixedUpdate()
    {
        isGrounded = rb.IsTouchingLayers();

        if (isDodging)
        {
            PerformDodge();
        }

        if (InputReceiver.Instance.GetBeastPlayerJumpInput() == 1 && isGrounded)
        {
            Jump();
        }

        if (InputReceiver.Instance.GetBeastPlayerDodgeInput() == 1 && !isDodging)
        {
            isDodging = true;
            dodgeStartTime = Time.time;
            dodgeDirection = GetDodgeDirection();
        }

        Move();
    }

    protected override void Move()
    {
        float dir = InputReceiver.Instance.GetBeastPlayerMoveDirection();

        rb.velocity = new Vector2(dir * moveSpeed, 0);
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
        isDodging = false;

        //finish animation
    }

    private Vector3 GetDodgeDirection()
    {
        Vector2 moveDirection = new Vector2(InputReceiver.Instance.GetBeastPlayerMoveDirection(),0);
        //if character does not move dash towards its own direction.Else dash according to movement.
        return !moveDirection.Equals(Vector2.zero) ? new Vector2(moveDirection.x, 0) : transform.forward;
    }
}
