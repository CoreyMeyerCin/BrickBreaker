using UnityEngine;

[CreateAssetMenu(fileName = "SoundTriggerSettings", menuName = "Sounds/Sound Trigger Settings")]
public class SoundTriggerSettings : ScriptableObject
{
    public AudioClip soundClip;
    public float volume = 1.0f;
}