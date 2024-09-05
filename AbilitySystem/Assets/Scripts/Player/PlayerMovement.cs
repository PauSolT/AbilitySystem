
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 6.0f;
    [SerializeField] float jumpSpeed = 8.0f;
    [SerializeField] float dashSpeed;


    BoxCollider2D coll;
    Rigidbody2D rb;

    Vector2 moveDirection = Vector2.zero;
    bool jumpPressed = false;

    bool isGrounded = false;
    bool disableMovement = false;
    public bool facingRight = true;

    public bool Dashing { get; set; } = false;

    public void SetRunSpeed(float runSpeed)
    {
        this.runSpeed = runSpeed;
    }

    public float GetRunSpeed()
    {
        return runSpeed;
    }

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

    }


    private void FixedUpdate()
    {
        if (disableMovement)
        {
            return;
        }

        HandleInput();
        UpdateIsGrounded();
        HandleMovement();
        HandleJumping();
        UpdateFacingDirection();

    }

    Element element;
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            GetComponent<HealthComponent>().TakeDamage(50, element);
            Debug.Log("Player damaged by 50!");
        }
    }

    private void HandleInput()
    {
        moveDirection = InputManager.Instance.GetMoveDirection();
        jumpPressed = InputManager.Instance.GetJumpPressed();
    }

    private void UpdateIsGrounded()
    {
        Bounds colliderBounds = coll.bounds;
        float colliderRadius = coll.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
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

    private void HandleMovement()
    {
        if (!Dashing)
        {
            rb.velocity = new Vector2(moveDirection.x * runSpeed, rb.velocity.y);
        }
        else if (Dashing)
        {
            rb.velocity = new Vector2(facingRight ? dashSpeed : -dashSpeed, 0);

        }
    }

    private void UpdateFacingDirection()
    {
        // set facing direction
        if (moveDirection.x > 0.1f)
        {
            facingRight = true;
        }
        else if (moveDirection.x < -0.1f)
        {
            facingRight = false;
        }
    }

    private void HandleJumping()
    {
        if (isGrounded && jumpPressed)
        {
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
}
