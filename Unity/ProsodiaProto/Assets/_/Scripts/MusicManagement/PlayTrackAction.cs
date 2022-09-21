using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrackAction : PuzzleActionHolder
{
    public AudioSource TrackToPlay;

    private void PlayTrack()
    {
        TrackToPlay.Play();
    }

    protected override void CreateAction()
    {
        Action = () =>
        {
            Debug.Log("track playing");
            PlayTrack();
        };
    }
}
