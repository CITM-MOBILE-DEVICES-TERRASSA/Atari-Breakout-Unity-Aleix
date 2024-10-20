using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 launchDirection = new Vector2(0, 1); // Dirección hacia arriba
    public float speed; // Velocidad de movimiento

    public float maxSpeed;
    bool launched;

    float minY = -5.10f;

    

    public int score = 0;

    

    public int lives = 3;

    public TextMeshProUGUI scoreText;

    public GameObject[] livesImage;

    Vector2 inicialPos;

    public float forceAmount;

    int brickCount;

    public int level;

    private LevelGenerator levelGenerator;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        // El Rigidbody es cinemático
        launched = false;

        inicialPos = body.position;

        level = 1;

    }

    private void Start()
    {
        brickCount = FindObjectOfType<LevelGenerator>().transform.childCount;
        levelGenerator = FindObjectOfType<LevelGenerator>();

        
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
            if(speed >= maxSpeed)
            {
                speed = maxSpeed;
            }
            body.velocity = body.velocity.normalized * speed;
        }

        if(body.position.y < minY)
        {
            transform.position = inicialPos;
            body.velocity = Vector3.zero;
            lives--;
            livesImage[lives].SetActive(false);
            launched = false;
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(speed < maxSpeed)
        {
            speed += 0.1f;
        }
        
        if (collision.gameObject.CompareTag("Brick"))
        {
            Brick brickScript = collision.gameObject.GetComponent<Brick>();
            if (brickScript.lives == 1)
            {
                Destroy(collision.gameObject);
                brickCount--;
                score++;
                scoreText.text = score.ToString("0000");
                Debug.Log("Brick count: " + brickCount);
                if (brickCount <= 0 && level == 1)
                {
                    level = 2;
                    levelGenerator.CreateLevel();
                    brickCount = FindObjectOfType<LevelGenerator>().transform.childCount - 1;
                    Debug.Log("Brick count: " + brickCount);
                    transform.position = inicialPos;
                    body.velocity = Vector3.zero;
                    launched = false;
                }
                else if (brickCount <= 0 && level == 2)
                {
                    level = 1;
                    levelGenerator.CreateLevel();
                    brickCount = FindObjectOfType<LevelGenerator>().transform.childCount -1;
                    Debug.Log("Brick count: " + brickCount);
                    transform.position = inicialPos;
                    body.velocity = Vector3.zero;
                    launched = false;
                }

            }
            else
            {
                brickScript.lives--;
                brickScript.GetComponent<SpriteRenderer>().color = Color.red;
            }
            
        }

        
    }

    
}
