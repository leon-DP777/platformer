using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    InputAction crouchAction;
    InputAction sprintAction;
    InputAction jumpAction;
    InputAction moveAction;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    public LayerMask groundLayerMask;
    bool isGrounded = false;
    bool result;
    bool isSprinting = false;
    bool isCrouching = false;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        groundLayerMask = LayerMask.GetMask("Ground");

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVel = moveAction.ReadValue<Vector2>();
        moveVel = moveVel * 2;
        rb.linearVelocity = new Vector2(moveVel.x, rb.linearVelocity.y);
        Jump();
        FlipSprite();
        Crouch();
        Sprint();
        isGrounded = RayCollisionCheck(0, 0);


        if (rb.linearVelocityX != 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if ((jumpAction.WasPressedThisFrame()) && (isGrounded == true))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 6);
        }
    }

    public bool RayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 1;
        bool hitSomething = false;

        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, groundLayerMask);
        Color hitColor = Color.red;

        if (hit.collider != null)
        {
            // print("Player has collided with Ground layer");
            hitColor = Color.green;
            hitSomething = true;
            isGrounded = true;
        }
        Debug.DrawRay(transform.position + offset, Vector2.down * rayLength, hitColor);
        return hitSomething;
    }

    void Crouch()
    {
        if (crouchAction.WasPerformedThisFrame() && !isCrouching)
        {
            isCrouching = true;
            anim.SetBool("crouch", true);
        }
        if ((crouchAction.WasPerformedThisFrame()) && isCrouching == true)
        {
            isCrouching = false;
            anim.SetBool("crouch", false);
        }
    }

    void Sprint()
    {
        if (sprintAction.WasPressedThisFrame())
        {
            rb.linearVelocityX = rb.linearVelocityX * 3;
        }    
    }

    void FlipSprite()
    {
        if (rb.linearVelocityX < -0.1f)
        {
            sr.flipX = true;
        }
        if (rb.linearVelocityX > 0.1f)
        {
            sr.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        while (collision.gameObject.name == "Player")
        {
            anim.SetBool("death", true);
            transform.position = new Vector2(x: (float)-6.55, (float)-1.88);
        }
    }
}
