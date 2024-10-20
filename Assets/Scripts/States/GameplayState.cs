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
            Debug.Log("GameOver");
            gameManager.SwitchState(new GameOverState());
            ball.lives = 3;
        }
    }

    public override void ExitState(GameManager gameManager)
    {
        // Nada específico al salir del gameplay
    }
}

