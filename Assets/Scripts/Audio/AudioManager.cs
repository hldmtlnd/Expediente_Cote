using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource ambientSource;
    public AudioSource voiceSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayAmbient(AudioClip clip, bool loop = true)
    {
        if (ambientSource == null) return;
        ambientSource.clip = clip;
        ambientSource.loop = loop;
        ambientSource.Play();
    }

    public void PlayVoice(AudioClip clip)
    {
        if (clip != null && voiceSource != null)
            voiceSource.PlayOneShot(clip);
    }

    public void StopAll()
    {
        sfxSource?.Stop();
        ambientSource?.Stop();
        voiceSource?.Stop();
    }
}
