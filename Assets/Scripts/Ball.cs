using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public GameObject ExperiencePointPrefab;

    public int experiencePoints = 0;

    RadialExperienceBar bar;

    public Button botonContinuar;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        // El Rigidbody es cinemático
        launched = false;

        launchable = false;

        inicialPos = body.position;

        level = 1;

        gameManager = FindObjectOfType<GameManager>();

        ballCollider = GetComponent<Collider2D>();

        bar = FindObjectOfType<RadialExperienceBar>();

    }

    private void Start()
    {
        brickCount = FindObjectOfType<LevelGenerator>().transform.childCount;
        levelGenerator = FindObjectOfType<LevelGenerator>();
        botonContinuar.interactable = PlayerPrefs.HasKey("UltimoNivel");

    }



    void Update()
    {
        if (ballCollider.isTrigger == false)
        {
            // Si aún no se ha lanzado
            if (launched)
            {

            }
            if (!launched)
            {
                if (launchable == true)
                {

                    if (Input.GetMouseButtonUp(0) && isPressing)
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
                if (speed >= maxSpeed)
                {
                    speed = maxSpeed;
                }
                body.velocity = body.velocity.normalized * speed;
            }

            if (body.position.y < minY)
            {
                transform.position = inicialPos;
                body.velocity = Vector3.zero;
                lives--;
                livesImage[lives].SetActive(false);
                launched = false;
            }
        }
        else
        {
            StartCoroutine(DisableTriggerAfterTime(7f));
        }
        

        
    }

    private IEnumerator DisableTriggerAfterTime(float time)
    {
        yield return new WaitForSeconds(time); // Espera el tiempo especificado
        ballCollider.isTrigger = false; // Desactiva el trigger
        Debug.Log("Trigger desactivado después de " + time + " segundos.");
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
                GameObject newExpiriencePoint = Instantiate(ExperiencePointPrefab);
                newExpiriencePoint.transform.position = collision.gameObject.transform.position;
                newExpiriencePoint.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 1, ForceMode2D.Impulse);
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
                GameObject newExpiriencePoint = Instantiate(ExperiencePointPrefab);
                newExpiriencePoint.transform.position = other.gameObject.transform.position;
                newExpiriencePoint.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 1, ForceMode2D.Impulse);
                Debug.Log("Instanciado punto de experiencia en: " + newExpiriencePoint.transform.position);
                Destroy(other.gameObject); // Destruir el brick
                brickCount--;
                score++;
                scoreText.text = score.ToString("0000");
                maxScoreHudText.text = gameManager.maxScore.ToString();
                Debug.Log("Destruyendo brick trigger, Brick count: " + brickCount);

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

                    GuardarPartida();
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

                    GuardarPartida();
                }
            }
            else
            {
                // Reducir vidas si el brick aún tiene más de 1 vida
                brickScript.lives--;
                brickScript.GetComponent<SpriteRenderer>().color = Color.red; // Cambiar color
            }
        }
        if (other.CompareTag("Up") || other.CompareTag("Player"))
        {
            Vector2 incomingDirection = body.velocity; // Dirección actual de la bola
            incomingDirection.y = -incomingDirection.y; // Invertir solo la componente y para rebote vertical
            body.velocity = incomingDirection;

            Debug.Log("Rebote en la pared superior con dirección: " + body.velocity);
        }

        // Lógica de rebote en 2D para los bordes laterales
        if (other.CompareTag("Borders"))
        {
            Vector2 incomingDirection = body.velocity; // Dirección actual de la bola
            Vector2 normal = (transform.position - other.transform.position).normalized; // Aproximación de la normal del borde

            // Aquí asumimos que el borde izquierdo está a la izquierda de la bola y el borde derecho a la derecha
            if (normal.x > 0) // Rebote en el borde izquierdo
            {
                incomingDirection.x = Mathf.Abs(incomingDirection.x); // Aseguramos que va hacia la derecha
            }
            else if (normal.x < 0) // Rebote en el borde derecho
            {
                incomingDirection.x = -Mathf.Abs(incomingDirection.x); // Aseguramos que va hacia la izquierda
            }

            // Aplicar la nueva dirección de rebote
            body.velocity = incomingDirection;

            Debug.Log("Rebote en borde lateral con dirección: " + body.velocity);
        }

    }

    IEnumerator ShowYouWinCanvas()
    {
        Debug.Log("win");
        WinCanvas.SetActive(true);  // Activamos el canvas
        yield return new WaitForSeconds(3);  // Esperamos 3 segundos
        WinCanvas.SetActive(false);  // Desactivamos el canvas
    }

    public void ActivateGhostMode()
    {
        if(bar.clickable)
        {
            Debug.Log("Ghost Mode");
            ballCollider.isTrigger = true;
            experiencePoints = 0;
        }
        
    }


    public void GuardarPartida()
    {
        PlayerPrefs.SetInt("MaximaPuntuacion", Mathf.Max(score, PlayerPrefs.GetInt("MaximaPuntuacion", 0)));
        PlayerPrefs.SetInt("Vidas", lives);
        PlayerPrefs.SetInt("UltimoNivel", level);
        PlayerPrefs.SetInt("PuntosExperiencia", experiencePoints);
        PlayerPrefs.Save();
    }

    private void CargarPartida()
    {
        // Cargar puntuación, vidas y nivel
        score = PlayerPrefs.GetInt("MaximaPuntuacion", 0);
        lives = PlayerPrefs.GetInt("Vidas", 3);
        level = PlayerPrefs.GetInt("UltimoNivel", 1);
        experiencePoints = PlayerPrefs.GetInt("PuntosExperiencia", 0);
        bar.AddExperiencePoint();

        // Actualiza el HUD
        scoreText.text = score.ToString("0000");
        maxScoreHudText.text = score.ToString("0000");

        // Actualiza las vidas visibles
        for (int i = 0; i < livesImage.Length; i++)
        {
            livesImage[i].SetActive(i < lives);
        }

        // Restaura la posición de la bola y el estado inicial
        transform.position = inicialPos;
        body.velocity = Vector3.zero;
        launched = false;
        launchable = true;

        // Restablece el nivel y cuenta de bloques
        levelGenerator = FindObjectOfType<LevelGenerator>();
        levelGenerator.CreateLevel(); // Asegura que los bloques en el nivel actual se generen
        brickCount = FindObjectOfType<LevelGenerator>().transform.childCount;
    }

    public void ContinuarPartida()
    {
        CargarPartida();
        // Carga el primer nivel o el último en el que el jugador estuvo
        FindObjectOfType<GameManager>().SwitchState(new GameplayState());  // Asegura el estado correcto
    }

}
