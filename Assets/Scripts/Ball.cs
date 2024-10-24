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
    public bool launched;

    float minY = -5.10f;

    

    public int score = 0;

    

    public int lives = 3;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI maxScoreHudText;

    public GameObject[] livesImage;

    Vector2 inicialPos;

    public float forceAmount;

    int brickCount;

    public int level;

    private LevelGenerator levelGenerator;

    public bool launchable;
    bool isPressing = false;

    GameManager gameManager;

    public GameObject WinCanvas;

      // Si estás usando físicas 2D
    private Collider2D ballCollider;  // Referencia al collider de la bola
    public bool isGhostMode = false;  // Modo atravesar objetos

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        // El Rigidbody es cinemático
        launched = false;

        launchable = false;

        inicialPos = body.position;

        level = 1;

        gameManager = FindObjectOfType<GameManager>();

    }

    private void Start()
    {
        brickCount = FindObjectOfType<LevelGenerator>().transform.childCount;
        levelGenerator = FindObjectOfType<LevelGenerator>();

        
    }



    void Update()
    {
        // Si aún no se ha lanzado
        if(launched)
        {

        }
        if (!launched)
        {
            if (launchable == true)
            {
              
                 if(Input.GetMouseButtonUp(0) && isPressing)
                {
                    // Lanzamos la bola y cambiamos el estado a 'lanzado'
                    body.AddForce(Vector2.up * forceAmount, ForceMode2D.Impulse);
                    Debug.Log("launched");
                    launched = true;
                    isPressing = false;
                }
                    
                
                if (Input.GetMouseButtonDown(0))
                {
                    isPressing = true;
                }
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
                maxScoreHudText.text = gameManager.maxScore.ToString();
                Debug.Log("Brick count: " + brickCount);
                if (brickCount <= 0 && level == 1)
                {
                    StartCoroutine(ShowYouWinCanvas());
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
                    StartCoroutine(ShowYouWinCanvas());
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entra en el trigger es un "Brick"
        if (other.CompareTag("Brick"))
        {
            Brick brickScript = other.GetComponent<Brick>(); // Obtener el script del brick

            if (brickScript.lives == 1)
            {
                Destroy(other.gameObject); // Destruir el brick
                brickCount--;
                score++;
                scoreText.text = score.ToString("0000");
                maxScoreHudText.text = gameManager.maxScore.ToString();
                Debug.Log("Destruyendo brick, Brick count: " + brickCount);

                // Lógica para cambiar de nivel
                if (brickCount <= 0 && level == 1)
                {
                    StartCoroutine(ShowYouWinCanvas());
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
                    StartCoroutine(ShowYouWinCanvas());
                    level = 1;
                    levelGenerator.CreateLevel();
                    brickCount = FindObjectOfType<LevelGenerator>().transform.childCount - 1;
                    Debug.Log("Brick count: " + brickCount);
                    transform.position = inicialPos;
                    body.velocity = Vector3.zero;
                    launched = false;
                }
            }
            else
            {
                // Reducir vidas si el brick aún tiene más de 1 vida
                brickScript.lives--;
                brickScript.GetComponent<SpriteRenderer>().color = Color.red; // Cambiar color
            }
        }
        
    }

    IEnumerator ShowYouWinCanvas()
    {
        Debug.Log("win");
        WinCanvas.SetActive(true);  // Activamos el canvas
        yield return new WaitForSeconds(3);  // Esperamos 3 segundos
        WinCanvas.SetActive(false);  // Desactivamos el canvas
    }

    


}
