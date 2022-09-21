using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyHolder : MonoBehaviour
{
    public Melody Melody;
    public int MelodyLength;
    public string SoundName = "Puzzle1";

    protected virtual void OnTriggerEnter(Collider other)
    {
        var playerMelodyManager = other.gameObject.GetComponent<PlayerMelodyManager>();
        playerMelodyManager.SetSound(SoundName);
    }
}
