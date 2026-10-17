using UnityEngine;

public class AlienAudio : MonoBehaviour
{
    public AudioClip footstepsClip;
    public AudioClip signsSoundClip;
    public AudioClip objectMoveClip;

    public AudioSource source;

    public void PlayFootsteps()
    {
        if (footstepsClip != null && source != null)
            source.PlayOneShot(footstepsClip);
    }

    public void PlaySignsSound()
    {
        if (signsSoundClip != null && source != null)
            source.PlayOneShot(signsSoundClip);
    }

    public void PlayObjectInteraction()
    {
        if (objectMoveClip != null && source != null)
            source.PlayOneShot(objectMoveClip);
    }
}
