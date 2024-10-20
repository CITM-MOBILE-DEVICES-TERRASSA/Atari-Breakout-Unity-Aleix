using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameState
{
    public override void EnterState(GameManager gameManager)
    {
        Debug.Log("Entering Pause State");
        Time.timeScale = 0f;
        gameManager.pauseCanvas.SetActive(true);
    }

    public override void UpdateState(GameManager gameManager)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameManager.SwitchState(new GameplayState());
        }
    }

    public override void ExitState(GameManager gameManager)
    {
        gameManager.pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}

