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

    Ball ball;

    public override void EnterState(GameManager gameManager)
    {
        Debug.Log("Entering Game Over State");
        gameManager.gameOverCanvas.SetActive(true);
        ball = FindObjectOfType<Ball>();
        finalScore = gameManager.puntuacion;

        if (finalScoreText != null)
        {
            finalScoreText.text = finalScore.ToString("0000");
        }

        // Desactivar el botón de continuar y eliminar la partida guardada
        Button botonContinuar = ball.botonContinuar;
        if (botonContinuar != null)
        {
            botonContinuar.interactable = false;  // Desactiva el botón
        }

        // Eliminar los datos de la partida guardada
        PlayerPrefs.DeleteKey("MaximaPuntuacion");
        PlayerPrefs.DeleteKey("Vidas");
        PlayerPrefs.DeleteKey("UltimoNivel");
        PlayerPrefs.Save();
        SoundManager.instance.PlayFx(SoundManager.instance.hitSound);
        SoundManager.instance.StopBackgroundMusic();
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

