using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controls : MonoBehaviour
{
    public KeyCode upMovement2 = KeyCode.UpArrow;
    public KeyCode downMovement2 = KeyCode.DownArrow;
    public float speed2 = 10.0f;
    public float boundY2 = 2.25f;
    private Rigidbody2D rigsby2;

    // Start is called before the first frame update
    void Start()
    {
        rigsby2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var velmer2 = rigsby2.velocity;
        if (Input.GetKey(upMovement2))
        {
            velmer2.y = speed2;
        }
        else if (Input.GetKey(downMovement2))
        {
            velmer2.y = -speed2;
        }
        else if (!Input.anyKey)
        {
            velmer2.y = 0;
        }
        rigsby2.velocity = velmer2;

        var possie2 = transform.position;
        if (possie2.y > boundY2)
        {
            possie2.y = boundY2;
        }
        else if (possie2.y < -boundY2)
        {
            possie2.y = -boundY2;
        }
        transform.position = possie2;
    }
}
