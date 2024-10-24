using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameOverState : GameState
{
    int finalScore;

    public TextMeshProUGUI finalScoreText;

    public override void EnterState(GameManager gameManager)
    {
        Debug.Log("Entering Game Over State");
        gameManager.gameOverCanvas.SetActive(true);

        // Iniciar un retraso para reiniciar el juego
        finalScore = gameManager.puntuacion;

        //if (finalScoreText != null)
        //{
        //    finalScoreText.text = finalScore.ToString("0000");
        //}
        //else
        //{
        //    Debug.LogError("finalScoreText no está asignado.");
        //}

        

    }

    private float elapsedTime = 0f; // Variable para almacenar el tiempo acumulado

    public override void UpdateState(GameManager gameManager)
    {
        // Incrementa el tiempo acumulado
        elapsedTime += Time.deltaTime;

        // Si se presiona la barra espaciadora o han pasado 5 segundos
        if (Input.GetKeyDown(KeyCode.Space) || elapsedTime >= 5f)
        {
            gameManager.CheckAndUpdateMaxScore();
            gameManager.RestartScene();
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

