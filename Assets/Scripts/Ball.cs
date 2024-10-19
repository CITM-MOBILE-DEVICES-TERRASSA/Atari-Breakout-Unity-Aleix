using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 launchDirection = new Vector2(0, 1); // Dirección hacia arriba
    public float speed = 5f; // Velocidad de movimiento
    bool launched;

    float minY = -5.10f;

    public float maxVelocity = 10f;

    int score = 0;
    int lives = 3;

    public TextMeshProUGUI scoreText;

    public GameObject[] livesImage;

    Vector2 inicialPos;

    public float forceAmount;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        // El Rigidbody es cinemático
        launched = false;

        

        inicialPos = body.position;

    }

    

    void Update()
    {
        // Si aún no se ha lanzado
        if (!launched)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // Lanzamos la bola y cambiamos el estado a 'lanzado'
                body.AddForce(Vector2.up * forceAmount, ForceMode2D.Impulse);
                launched = true;
            }
        }
        else
        {

            
        }

        if(body.position.y < minY)
        {
            transform.position = inicialPos;
            body.velocity = Vector3.zero;
            lives--;
            livesImage[lives].SetActive(false);
            launched = false;
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
            scoreText.text = score.ToString("0000");
        }

        
    }
}
