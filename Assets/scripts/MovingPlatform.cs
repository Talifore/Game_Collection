using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] Destination;
    //public Vector3 Startp;
    //public Vector3 Endp;
    public float Speed;
    private bool dir = true;
    private float timer;
    public float breakTime;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Destination[0];
        index = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0 )
        {
            timer = timer - Time.deltaTime;
            return;
        }
        else
        {
            Vector3 destination = Destination[index];
            MovePlatform(destination);
            if (this.transform.position == destination)
            {
                timer = breakTime;
                if (index == Destination.Length - 1 || index == 0)
                {
                    dir = !dir;
                }
                index += dir ? +1 : -1;
            }
        }
    }

    void MovePlatform(Vector3 dest)
    {
        Vector3 direction = dest - this.transform.position;
        float speed = Mathf.Min(Speed * Time.deltaTime, direction.magnitude);
        direction.Normalize();
        this.transform.position += direction * speed;
    }
}
