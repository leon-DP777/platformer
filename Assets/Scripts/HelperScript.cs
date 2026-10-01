using UnityEngine;

public class HelperScript : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
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
    public void FlipOnMove()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (rb.linearVelocityX < -0.1f)
        {
            sr.flipX = true;
        }
        if (rb.linearVelocityX > 0.1f)
        {
            sr.flipX = false;
        }
    }

}
