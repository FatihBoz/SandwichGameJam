using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    bool isGrounded;

    private Rigidbody2D rb;

    bool canAttack = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    private void Move()
    {
        float moveDir = InputReceiver.Instance.GetBeastPlayerMoveDirection();

        rb.velocity = new Vector2(moveDir * moveSpeed, 0);

    }


    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed);
    }

    private void FixedUpdate()
    {
        isGrounded = rb.IsTouchingLayers();

        if (InputReceiver.Instance.GetBeastPlayerJumpInput() == 1 && isGrounded)
        {
            Jump();
        }

        Move();
    }
}
