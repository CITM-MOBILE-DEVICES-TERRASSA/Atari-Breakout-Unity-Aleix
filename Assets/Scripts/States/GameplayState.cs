using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameplayState : GameState
{

    Ball ball;
    public override void EnterState(GameManager gameManager)
    {
        Debug.Log("Entering Gameplay State");
        Time.timeScale = 1f;
        ball = FindObjectOfType<Ball>();
        ball.launchable = true;
    }

    public override void UpdateState(GameManager gameManager)
    {
        // Pausar el juego
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameManager.SwitchState(new PauseState());
        }
        // Lógica de gameover (ejemplo)
        if (ball.lives <= 0)
        {
            
            
            PlayerPrefs.Save();
            Debug.Log("GameOver");
            GameManager.FindObjectOfType<GameManager>().puntuacion = Ball.FindObjectOfType<Ball>().score;
            Debug.Log("punt count: " + GameManager.FindObjectOfType<GameManager>().puntuacion);
            gameManager.SwitchState(new GameOverState());
            
            ball.lives = 3;
        }
    }

    public override void ExitState(GameManager gameManager)
    {
        // Nada específico al salir del gameplay
    }
}

