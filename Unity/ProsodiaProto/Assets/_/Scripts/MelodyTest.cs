using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyTest : MonoBehaviour
{
  MelodyGenerator generator;
  void Start()
  {
    generator = new MelodyGenerator();
    Debug.Log(generator.GetNewMelody(4));
  }

  // Update is called once per frame
  void Update()
  {

  }
}
