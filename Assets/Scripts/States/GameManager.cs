using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameState currentState;
    public GameObject startCanvas;
    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;

    public int playerHealth = 3;

    public Button restart;

    public Button despause;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentState = new StartState();
        currentState.EnterState(this);


    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void RestartScene()
    {
        gameOverCanvas.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Awake();
    }

    public void Pause()
    {

        SwitchState(new PauseState());

    }
    public void Despause()
    {

        SwitchState(new GameplayState());

    }

    public void Beggin()
    {
        SwitchState(new GameplayState());
    }
}
