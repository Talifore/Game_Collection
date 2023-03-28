using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rudy;

    void GoBallGo()
    {
        float randy = Random.Range(0, 2);
        if (randy < 1)
        {
            rudy.AddForce(new Vector2(20, -15));
        }
        else
        {
            rudy.AddForce(new Vector2(-20, -15));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rudy = GetComponent<Rigidbody2D>();
        Invoke("GoBallGo", 2);
    }

    void ResetBall()
    {
        rudy.velocity =  new Vector2 (0, 0);
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBallGo", 1);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag ("Player"))
        {
            Vector2 vel;
            vel.x = rudy.velocity.x;
            vel.y = (rudy.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rudy.velocity = vel;
        }
    }
}
