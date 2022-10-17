using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FinishGameAction : ActionHolder
{

    private void FinishGame()
    {
        GetComponent<CameraManager>().enabled = false;
        GetComponent<CharacterMovement>().enabled = false;
    }

    protected override void CreateAction()
    {
        Action = () =>
        {
            FinishGame();
        };
    }
}