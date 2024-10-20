using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public Vector2Int size;
    public Vector2 offset;
    public GameObject brickPrefab;
    public Gradient gradient;

    

    private void Awake()
    {
        CreateLevel();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateLevel()
    {
        if (Ball.FindObjectOfType<Ball>().level == 1)
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {

                    GameObject newBrick = Instantiate(brickPrefab, transform);
                    newBrick.transform.position = transform.position + new Vector3(i - 1 * offset.x, j * offset.y, 0);

                    // Obtener el componente 'Brick' y modificar la variable 'lives'
                    Brick brickScript = newBrick.GetComponent<Brick>();  // Obtener el script Brick
                    if (brickScript != null)
                    {
                        if (newBrick.transform.position.y > 1.20f)
                        {
                            brickScript.lives = 2;  // Cambiar el número de vidas
                            newBrick.GetComponent<SpriteRenderer>().color = Color.blue;
                        }
                        else
                        {
                            brickScript.lives = 1;
                            newBrick.GetComponent<SpriteRenderer>().color = Color.red;
                        }
                    }
                }
            }

        }
        else
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {

                    GameObject newBrick = Instantiate(brickPrefab, transform);
                    newBrick.transform.position = transform.position + new Vector3(i - 1 * offset.x, j * offset.y, 0);

                    // Obtener el componente 'Brick' y modificar la variable 'lives'
                    Brick brickScript = newBrick.GetComponent<Brick>();  // Obtener el script Brick
                    if (brickScript != null)
                    {
                        if (newBrick.transform.position.y > 0f)
                        {
                            brickScript.lives = 2;  // Cambiar el número de vidas
                            newBrick.GetComponent<SpriteRenderer>().color = Color.blue;
                        }
                        else
                        {
                            brickScript.lives = 1;
                            newBrick.GetComponent<SpriteRenderer>().color = Color.red;
                        }
                    }
                }
            }
        }
    }
}
