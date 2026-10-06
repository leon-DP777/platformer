using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{

    HelperScript helper;
    InputAction crouchAction;
    InputAction sprintAction;
    InputAction jumpAction;
    InputAction moveAction;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    InputAction attackAction;
    public LayerMask groundLayerMask;
    bool isGrounded = false;
    bool result;
    public Camera MainCamera;
    public GameObject weapon;
    public GameObject Player;
    //  bool isSprinting = false;
    bool isCrouching = false;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        attackAction = InputSystem.actions.FindAction("Attack");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        groundLayerMask = LayerMask.GetMask("Ground");
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        helper = gameObject.AddComponent<HelperScript>();
        MainCamera = Camera.main;



    }

    // Update is called once per frame
    void Update()
    {
        MoveVelocity();
        Jump();
        ThrowWeapon();
        Crouch();
        Sprint();
        SetAnim();
        CallHelper();
        Grounding();
    }
    void ThrowWeapon()
    {
        if (attackAction.WasPressedThisFrame())
        {
            GameObject clone;
            clone = weapon;
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            if (helper.isLeft == true && (helper.isRight == false))
            {
                clone = Instantiate(weapon, transform.position, Quaternion.Euler(0, 0, 0));
                rb.linearVelocity = new(-15, 0);
                rb.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
            }
            if (helper.isRight == true && (helper.isLeft == false))
            {
                clone = Instantiate(weapon, transform.position, Quaternion.Euler(0, 0, 180));
                rb.linearVelocity = new Vector2(15, 0);
                rb.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
            }
        }
    }
    void MoveVelocity()
    {
        Vector2 moveVel = moveAction.ReadValue<Vector2>();
        moveVel = moveVel * 2;
        rb.linearVelocity = new Vector2(moveVel.x, rb.linearVelocity.y);
    }

    void Grounding()
    {
        isGrounded = RayCollisionCheck(0, 0);
    }

    void CallHelper()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && (sr.flipX == false))
        {
            helper.FlipSprite(true);
        }
        if (Keyboard.current.fKey.wasPressedThisFrame && (sr.flipX == true))
        {
            helper.FlipSprite(false);
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            helper.DestroyObject(true);
        }
        helper.FlipOnMove();
    }

    void SetAnim()
    {
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
        float rayLength = 0.1f;
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
        if (crouchAction.WasPressedThisFrame() && !isCrouching)
        {
            isCrouching = true;
            anim.SetBool("crouch", true);
        }
        if ((crouchAction.WasPressedThisFrame()) && isCrouching == true)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Enemy")
        {
            return;
        }
        if (collision.gameObject.name == "Enemy")
        {
            GameObject player;
            player = gameObject;
            Destroy(player);
            Vector3 spawnPos = new (-6.5f, -1.5f, 0);
            player = Instantiate(player, spawnPos, Quaternion.Euler(0,0,0));
        }



    }

}
