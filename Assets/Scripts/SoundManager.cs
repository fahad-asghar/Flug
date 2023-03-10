using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip buttonClick1;
    public AudioClip swoosh;
    public AudioClip notification;
   
    private void Awake()
    {
        instance = this;
    }

    public void playSound(AudioClip audio)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = audio;
        source.Play();

        Destroy(source, audio.length + 0.1f);
    }

    public void playSound(AudioClip audio, float volume)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.volume = volume;
        source.clip = audio;
        source.Play();

        Destroy(source, audio.length + 0.1f);
    }
}