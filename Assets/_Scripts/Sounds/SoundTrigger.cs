using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public SoundTriggerSettings soundSettings;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundSettings.soundClip;
        audioSource.volume = soundSettings.volume;
    }

    public void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (audioSource.clip != null)
        {
            audioSource.Stop();
            audioSource.Play();
        }
    }
}
