using System.Diagnostics;
using UnityEngine;

public class EnemyTwoScript : MonoBehaviour
{
    public GameObject player;
    public GameObject EnemyTwo;
    SpriteRenderer sr;
    HelperScript helper;
    void Start()
    {
        InvokeRepeating(nameof(MoveTo), 1, 0.1f);
        sr = GetComponent<SpriteRenderer>();
        helper = GetComponent<HelperScript>();

    }

    // Update is called once per frame
    void Update()
    {
        //print("Player x position is " + player.transform.position.x);
        FlipSprite();
        //helper.FlipOnMove();
    }

    void FlipSprite()
    {
        if (player.transform.position.x > EnemyTwo.transform.position.x)
        {
            sr.flipX = true;
        }

        if (player.transform.position.x < EnemyTwo.transform.position.x)
        {
            sr.flipX = false;
        }

    }


    void MoveTo()
    {
        float goalPosA = Mathf.MoveTowards(EnemyTwo.transform.position.x , player.transform.position.x + 1.1f, 0.03f);
        float goalPosB = Mathf.MoveTowards(EnemyTwo.transform.position.x , player.transform.position.x - 1.1f, 0.03f);

        if (EnemyTwo.transform.position.x > player.transform.position.x)
        {
            EnemyTwo.transform.position = new Vector2(goalPosA, EnemyTwo.transform.position.y);
        }
        if (EnemyTwo.transform.position.x < player.transform.position.x)
        {
            EnemyTwo.transform.position = new Vector2(goalPosB, EnemyTwo.transform.position.y);
        }
    }
}

