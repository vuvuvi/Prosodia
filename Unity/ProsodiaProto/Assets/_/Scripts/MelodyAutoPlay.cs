using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyAutoPlay : MonoBehaviour
{
    private MelodyHolder holder;
    void Start()
    {
        holder = GetComponent<MelodyHolder>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var audioManager = other.gameObject.GetComponentInChildren<AudioManager>();
        if (audioManager)
        {
            audioManager.Play(holder.Melody);
        }
    }
}
