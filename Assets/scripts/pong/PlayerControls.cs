using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public KeyCode upMovement = KeyCode.W;
    public KeyCode downMovement = KeyCode.S;
    public float speed = 10.0f;
    public float boundY = 2.25f;
    private Rigidbody2D rigsby;

    // Start is called before the first frame update
    void Start()
    {
        rigsby = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var velmer = rigsby.velocity;
        if (Input.GetKey(upMovement))
        {
            velmer.y = speed;
        }
        else if (Input.GetKey(downMovement))
        {
            velmer.y = -speed;
        }
        else if(!Input.anyKey)
        {
            velmer.y = 0;
        }
        rigsby.velocity = velmer;

        var possie = transform.position;
        if(possie.y > boundY)
        {
            possie.y = boundY;
        }
        else if(possie.y < -boundY)
        {
            possie.y = -boundY;
        }
        transform.position = possie;
    }
}
