using UnityEngine;

public class HelperScript : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    public bool isLeft = false;
    public bool isRight = false;
    public void FlipSprite(bool flip)
    {


        sr = gameObject.GetComponent<SpriteRenderer>();
        if (flip == true)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    public void DestroyObject(bool destroy)
    {
        if (destroy == true)
        {
            Destroy(gameObject);
        }
    }

    public void OnFlip()
    {
        if (sr.flipX == false)
        {
            isLeft = false;
            isRight = true;
        }
        if (sr.flipX == true)
        {
            isLeft = true;
            isRight = false;
        }
    }

    public void FlipOnMove()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
            if (rb.linearVelocityX < -0.1f && (rb.linearVelocityX != 0))
        {
            sr.flipX = true;
        }
        else if (rb.linearVelocityX > 0.1f && (rb.linearVelocityX != 0))
        {
            sr.flipX = false;
        }
    }

}
