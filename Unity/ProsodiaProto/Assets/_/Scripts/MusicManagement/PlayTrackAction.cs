using UnityEngine;

public class PlayTrackAction : ActionHolder
{
    public AudioSource TrackToPlay;
    public float Delay;

    private void PlayTrack()
    {
        TrackToPlay.PlayDelayed(Delay);
    }

    protected override void CreateAction()
    {
        Action = () =>
        {
            PlayTrack();
        };
    }
}