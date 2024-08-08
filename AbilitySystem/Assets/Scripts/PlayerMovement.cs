using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 6.0f;
    [SerializeField] float jumpSpeed = 8.0f;

    CapsuleCollider coll;
    Rigidbody rb;

    Vector2 moveDirection = Vector2.zero;
    bool jumpPressed = false;

    bool isGrounded = false;
    bool disableMovement = false;


    Vector3 direction = Vector3.zero;
    float offset = 0;
    Vector3 localPoint0 = Vector3.zero;
    Vector3 localPoint1 = Vector3.zero;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        direction = new Vector3 { [coll.direction] = 1 };
        offset = coll.height / 2 - coll.radius;
    }

    private void FixedUpdate()
    {
        if (disableMovement)
        {
            return;
        }

        HandleInput();

        UpdateIsGrounded();

        HandleHorizontalMovement();

        HandleJumping();
    }

    private void HandleInput()
    {
        moveDirection = InputManager.Instance.GetMoveDirection();
        jumpPressed = InputManager.Instance.GetJumpPressed();
    }

    private void UpdateIsGrounded()
    {
        localPoint0 = transform.TransformPoint(coll.center - direction * offset);
        localPoint1 = transform.TransformPoint(coll.center + direction * offset);
        Collider[] colliders = Physics.OverlapCapsule(localPoint0, localPoint1, coll.radius);
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != coll)
                {
                    isGrounded = true;
                    i = colliders.Length;
                }
            }
        }
    }

    private void HandleHorizontalMovement()
    {
        rb.velocity = new Vector3(moveDirection.x * runSpeed, rb.velocity.y, moveDirection.y * runSpeed);
    }

    private void HandleJumping()
    {
        if (isGrounded && jumpPressed)
        {
            isGrounded = false;
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }
    }
}
