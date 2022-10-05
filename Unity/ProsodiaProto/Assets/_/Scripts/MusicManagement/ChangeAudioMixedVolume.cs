using UnityEngine;
using UnityEngine.Audio;

public class ChangeAudioMixedVolume : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public string GroupName;
    public float DecibelChange;
    private bool isLow;

    public void ChangeVolume()
    {
        AudioMixer.GetFloat(GroupName, out var oldValue);
        AudioMixer.SetFloat(GroupName,oldValue + (isLow ? 1 : -1) * DecibelChange);
        isLow = !isLow;
    }
}