using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpSpeed;
    [SerializeField] private Transform raycastShootPoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayLength;

    protected bool isGrounded;

    protected Rigidbody2D rb;
    protected Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    protected virtual void Move()
    {
        float moveDir = InputReceiver.Instance.GetBeastPlayerMoveDirection();

        rb.velocity = new Vector2(moveDir * moveSpeed,0);

    }

    protected bool GroundCheck()
    {
        return Physics2D.Raycast(raycastShootPoint.position, Vector2.down, rayLength, groundLayer);
    }


    protected void Jump()
    {
        rb.velocity = new Vector2(0, jumpSpeed);
    }

    protected void Animate()
    {
        animator.SetFloat("Speed", rb.velocity.x);
    }

    protected virtual void FixedUpdate()
    {
        isGrounded = GroundCheck();

        if (InputReceiver.Instance.GetBeastPlayerJumpInput() == 1 && isGrounded)
        {
            Jump();
        }

        Move();
        Animate();
    }
}
