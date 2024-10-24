using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f; // Velocidad del movimiento

    bool automatic = false;

    bool way = false;

    Ball ball;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        Vector2 velocity = rb.velocity;

        

        if(automatic)
        {
            Rigidbody2D ballRb = ball.GetComponentInParent<Rigidbody2D>();

            // Mantén la posición actual en Y y solo iguala la X
            if(way)
            {
                rb.position = new Vector2(ballRb.position.x + 0.1f, rb.position.y);
            }
            else
            {
                rb.position = new Vector2(ballRb.position.x - 0.1f, rb.position.y);
            }
            
            
        }
        else
        {
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

            if (Input.GetMouseButton(0)) // 0 es el botón izquierdo del mouse
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Convertir la posición del mouse a coordenadas del mundo
                Vector2 newPosition = new Vector2(mousePosition.x, rb.position.y); // Mantener la misma posición en y

                // Mover el objeto hacia la nueva posición
                rb.position = Vector2.Lerp(rb.position, newPosition, moveSpeed * Time.deltaTime);

                if(!ball.launched)
                {
                    Vector3 newPositionn = ball.transform.position;
                    newPositionn.x = rb.position.x;
                    ball.transform.position = newPositionn;
                }
                


                

                rb.velocity = velocity;
            }
            //if (Input.GetMouseButtonUp(0))
            //{
            //    GameManager gameManager = FindObjectOfType<GameManager>();

            //    if (!gameManager.startCanvas.activeSelf && !gameManager.gameOverCanvas.activeSelf && !gameManager.pauseCanvas.activeSelf)
            //    {
            //        Debug.Log("launched");
                    
            //    }



            //}
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            way = !way;
        }


    }

    public void AutomaticSwitch()
    {
        
            automatic = !automatic;
        
    }
}
