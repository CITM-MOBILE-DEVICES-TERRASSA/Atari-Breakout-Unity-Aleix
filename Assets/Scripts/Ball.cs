using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 launchDirection = new Vector2(0, 1); // Dirección hacia arriba
    public float speed = 5f; // Velocidad de movimiento
    bool launched;

    float minY = -5.10f;

    public float maxVelocity = 10f;

    int score = 0;
    int lives = 5;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        // El Rigidbody es cinemático
        launched = false;
    }

    void Update()
    {
        // Si aún no se ha lanzado
        if (!launched)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // Lanzamos la bola y cambiamos el estado a 'lanzado'
                body.position += launchDirection * speed * Time.deltaTime;
                launched = true;
            }
        }
        else
        {

            // Continuar moviendo la bola en la dirección de lanzamiento
            body.position += launchDirection * speed * Time.deltaTime;
        }

        if(body.position.y < minY)
        {
            transform.position = Vector3.zero;
            body.velocity = Vector3.zero;
            lives--;
        }

        if (body.velocity.magnitude > maxVelocity)
        {
            body.velocity = Vector3.ClampMagnitude(body.velocity, maxVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            score++;
        }

        
    }
}
