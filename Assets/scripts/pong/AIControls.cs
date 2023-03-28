using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControls : MonoBehaviour
{
    public float speedAI = 5.0f;
    public float boundYAI = 2.25f;
    private Rigidbody2D rigsbyAI;
    GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        rigsbyAI = GetComponent<Rigidbody2D>();
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        var velmerAI = rigsbyAI.velocity;
        if(ball.gameObject.transform.position.y > rigsbyAI.position.y)
        {
            velmerAI.y = speedAI;
        }
        else
        {
            velmerAI.y = -speedAI;
        }
        rigsbyAI.velocity = velmerAI;

        var possieAI = transform.position;
        if (possieAI.y > boundYAI)
        {
            possieAI.y = boundYAI;
        }
        else if (possieAI.y < -boundYAI)
        {
            possieAI.y = -boundYAI;
        }
        transform.position = possieAI;
    }
}
