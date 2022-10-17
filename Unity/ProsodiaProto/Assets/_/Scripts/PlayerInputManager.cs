using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private PlayerMelodyManager playerMelodyManager;
    private Keyboard keyboard;
    private GameManager gameManager;

    private void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        playerMelodyManager = GetComponent<PlayerMelodyManager>();
        gameManager = FindObjectOfType<GameManager>();
        keyboard = new Keyboard();
        keyboard.Enable();
        SetUpBindings();
    }

    private void SetUpBindings()
    {
        keyboard.PlayerMusic.Key0.performed += ctx => playerMelodyManager.AddNote(0);
        keyboard.PlayerMusic.Key1.performed += ctx => playerMelodyManager.AddNote(1);
        keyboard.PlayerMusic.Key2.performed += ctx => playerMelodyManager.AddNote(2);
        keyboard.PlayerMusic.Key3.performed += ctx => playerMelodyManager.AddNote(3);
        keyboard.PlayerMusic.Key4.performed += ctx => playerMelodyManager.AddNote(4);
        keyboard.PlayerMusic.Key5.performed += ctx => playerMelodyManager.AddNote(5);
        keyboard.PlayerMusic.Key6.performed += ctx => playerMelodyManager.AddNote(6);
        keyboard.PlayerMusic.Key7.performed += ctx => playerMelodyManager.AddNote(7);
        keyboard.PlayerMusic.Key8.performed += ctx => playerMelodyManager.AddNote(8);
        keyboard.PlayerMusic.Key9.performed += ctx => playerMelodyManager.AddNote(9);
        keyboard.PlayerMove.ToggleMovePlay.performed += ctx => characterMovement.ToggleMovePlay();
        keyboard.PlayerMove.Move0.performed += ctx => characterMovement.MoveTo(0);
        keyboard.PlayerMove.Move1.performed += ctx => characterMovement.MoveTo(1);
        keyboard.PlayerMove.Move2.performed += ctx => characterMovement.MoveTo(2);
        keyboard.PlayerMove.Move3.performed += ctx => characterMovement.MoveTo(3);
        keyboard.PlayerMove.Echolocation.performed += ctx => characterMovement.Echolocation();
        keyboard.PlayerMove.RestartGame.performed += ctx => gameManager.RestartGame();
    }
}