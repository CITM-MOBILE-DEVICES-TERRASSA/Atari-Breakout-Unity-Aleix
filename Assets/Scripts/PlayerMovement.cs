using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f; // Velocidad del movimiento

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 velocity = rb.velocity;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity.x = moveSpeed; // Mover a la derecha
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x = -moveSpeed; // Mover a la izquierda
        }
        else
        {
            velocity.x = 0; // Detener el movimiento horizontal si no se presiona ninguna tecla
        }

        rb.velocity = velocity;
    }
}
