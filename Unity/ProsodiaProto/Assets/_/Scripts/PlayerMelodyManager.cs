using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerMelodyManager : MonoBehaviour
{
  public Melody CurrentMelody;
  public UnityEvent MelodyChanged;

  public UnityEvent<int> NotePlayed;

  private void Start()
  {
    CurrentMelody = new Melody();
    MelodyChanged = new UnityEvent();
  }
  private void AddNote(int note)
  {
    CurrentMelody.AddNote(note);
    MelodyChanged.Invoke();
    NotePlayed.Invoke(note);
  }
  public void Reset()
  {
    CurrentMelody.Reset();
  }

  void OnKey0()
  {
    AddNote(0);
  }

  void OnKey1()
  {
    AddNote(1);
  }

  void OnKey2()
  {
    AddNote(2);
  }

  void OnKey3()
  {
    AddNote(3);
  }

  void OnKey4()
  {
    AddNote(4);
  }

  void OnKey5()
  {
    AddNote(5);
  }

  void OnKey6()
  {
    AddNote(6);
  }

  void OnKey7()
  {
    AddNote(7);
  }

  void OnKey8()
  {
    AddNote(8);
  }

  void OnKey9()
  {
    AddNote(9);
  }
}
