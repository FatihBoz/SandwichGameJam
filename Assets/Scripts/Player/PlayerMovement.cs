using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpSpeed;
    [SerializeField] private Transform raycastShootPoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayLength;

    //[Header("***AUDIO***")]
    //[SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioClip footStepClip;

    protected bool isGrounded;

    protected Rigidbody2D rb;
    protected Animator animator;
    protected float playerScale;
    protected bool isRunning;


    protected bool isStopped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playerScale = transform.localScale.x;  
    }
    protected virtual void Move()
    {
        float moveDir = InputReceiver.Instance.GetHumanPlayerMoveDirection();

        rb.velocity = new Vector2(moveDir * moveSpeed,rb.velocity.y);

        
    }


    protected virtual void RotatePlayer()
    {
        if (rb.velocity.x > 0.1f)
        {
            transform.localScale = new (-playerScale, playerScale);
        }
        else if (rb.velocity.x < -0.1f)
        {
            transform.localScale = new (playerScale, playerScale);
        }
    }

    protected bool GroundCheck()
    {
        return Physics2D.Raycast(raycastShootPoint.position, Vector2.down, rayLength, groundLayer);
    }


    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

    protected void Animate()
    {
        animator.SetBool("isRunning", !(rb.velocity.x == 0));
    }

    protected virtual void FixedUpdate()
    {
        if (isStopped)
        {
            rb.velocity=Vector2.zero;
            return;
        }

        isGrounded = GroundCheck();

        if (InputReceiver.Instance.GetBeastPlayerJumpInput() == 1 && isGrounded)
        {
            Jump();
        }

        Move();
        RotatePlayer();
        Animate();
    }

    //animation event
    //public void FootStep()
    //{
    //    audioSource.PlayOneShot(footStepClip);
    //}

    public void SetIsStopped(bool isStopped)
    {
        this.isStopped=isStopped;
    }
}
