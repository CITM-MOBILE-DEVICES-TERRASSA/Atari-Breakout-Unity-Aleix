using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverState : GameState
{


    public override void EnterState(GameManager gameManager)
    {
        Debug.Log("Entering Game Over State");
        gameManager.gameOverCanvas.SetActive(true);

        // Iniciar un retraso para reiniciar el juego


        Time.timeScale = 0f;

    }

    public override void UpdateState(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.SwitchState(new GameplayState());

        }
        // No se necesita update, el juego se reinicia tras el retraso
    }

    public override void ExitState(GameManager gameManager)
    {
        gameManager.gameOverCanvas.SetActive(false);
    }

    public void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");

    }
}

