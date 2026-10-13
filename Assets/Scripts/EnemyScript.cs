using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyScript : MonoBehaviour
{
    HelperScript helper;
    Rigidbody2D rb;
    CapsuleCollider2D cc;
    Animator anim;
    SpriteRenderer sr;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    // bool isGrounded = false;
    bool isLeft;
    bool isRight;
    //bool atLeft = false;
    //bool atRight = false;
    bool result;
    float dirx;
    public float timer = 8f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        groundLayerMask = LayerMask.GetMask("Default");
        wallLayerMask = LayerMask.GetMask("Wall");
        helper = gameObject.AddComponent<HelperScript>();
        // StartCoroutine(Patrol());

    }

    // Update is called once per frame
    void Update()
    {
        // FlipSprite();
        SetAnim();


        isLeft = RayCollisionCheck(-0.4f, 1);
        isRight = RayCollisionCheck(1f, 1);
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            helper.FlipSprite(true);
        }
        helper.FlipOnMove();
        Patrol();
    }

    void SetAnim()
    {
        if (rb.linearVelocityX != 0)
        {
            anim.SetBool("enemywalk", true);
        }
        else
        {
            anim.SetBool("enemywalk", false);
        }
    }


    public bool RayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.5f;
        bool hitSomething = false;

        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        RaycastHit2D hitGround;

        hitGround = Physics2D.Raycast(transform.position + offset, Vector2.left, rayLength, groundLayerMask);
        Color hitColor = Color.red;

        if (hitGround.collider != null)
        {
            // print("Player is in sight of enemy");
            hitColor = Color.green;
            hitSomething = true;
        }
        Debug.DrawRay(transform.position + offset, Vector2.left * rayLength, hitColor);
        return hitSomething;
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


    public void CalculateDirection()
    {
        if (rb.linearVelocityX < 0)
        {
            isLeft = true;
            isRight = false;
        }
        if (rb.linearVelocityX > 0)
        {
            isLeft = false;
            isRight = true;
        }
    }

    public void CalculateMovement()
    {
        if (isLeft.Equals(true))
        {
            dirx = -1;
            rb.linearVelocity = new Vector2(dirx, rb.linearVelocity.y);
        }
        if (isLeft.Equals(false))
        {
            dirx = 1;
            rb.linearVelocity = new Vector2(dirx, rb.linearVelocity.y);
        }
        if ((isLeft.Equals(false)) && (isRight.Equals(false)))
        {
            dirx = 0;
            rb.linearVelocity = new Vector2(dirx, rb.linearVelocityY);
        }
        dirx = 0;
    }

    /* IEnumerator Patrol()
     {
         atLeft = true;
         atRight = false;
         float moveLeft = Mathf.MoveTowards(transform.position.x, 3.2f, 0.5f);
         float moveRight = Mathf.MoveTowards(transform.position.x, 6.8f, 0.5f);
         if (atLeft == true || (atRight = false))
         {
             while (transform.position.x != 6.8f)
             {
                 rb.linearVelocityX = -0.5f;
                 transform.position = new Vector2(moveLeft, transform.position.y);
                 atLeft = false;
                 atRight = true;
             }

         }

         yield return new WaitForSeconds(8);

         if (atLeft == false || (atRight = true))
         {
             while (transform.position.x != 6.8f)
             {
                 rb.linearVelocityX = 0.5f;
                 transform.position = new Vector2(moveRight, transform.position.y);
                 atLeft = true;
                 atRight = false;
             }

         }



     }

    */

    void Patrol()
    {
        float left = Mathf.MoveTowards(transform.position.x, 3.2f, 0.003f);
        float right = Mathf.MoveTowards(transform.position.x, 6.8f, 0.003f);

        if (timer > 0)
        {
            transform.position = new Vector2(left, transform.position.y);
        }
        print(timer);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            transform.position = new Vector2(right, transform.position.y);
            if (transform.position.x == 6.8f && timer < -8f)
            {
                timer = 8f;
            }
        }
    }

}

