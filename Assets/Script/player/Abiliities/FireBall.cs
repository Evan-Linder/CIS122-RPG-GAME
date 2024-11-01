using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 3.5f; // Speed of the fireball
    public int direction; // Direction of movement (0: Up, 1: Right, 2: Down, 3: Left)

    void Start()
    {
        // Destroy the fireball after 5 seconds to prevent it from existing indefinitely
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        MoveFireBall();
    }

    void MoveFireBall()
    {
        // move based on the direction
        if (direction == 0) 
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else if (direction == 1)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (direction == 2) 
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (direction == 3) 
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destroy fireball on collision with an enemy
        }
    }
}




