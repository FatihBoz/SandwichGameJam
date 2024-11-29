using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpSpeed;
    protected bool isGrounded;

    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Move()
    {
        float moveDir = InputReceiver.Instance.GetBeastPlayerMoveDirection();

        rb.velocity = new Vector2(moveDir * moveSpeed, 0);

    }


    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed);
    }

    protected virtual void FixedUpdate()
    {
        isGrounded = rb.IsTouchingLayers();

        if (InputReceiver.Instance.GetBeastPlayerJumpInput() == 1 && isGrounded)
        {
            Jump();
        }

        Move();
    }
}
