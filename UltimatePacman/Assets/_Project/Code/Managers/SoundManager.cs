using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioClip chasingClip = null;
    [SerializeField]
    private AudioClip fleeingClip = null;
    [SerializeField]
    private AudioClip retreatingClip = null;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Methods for changing current music
    public void ChasingSound() => PlaySound(chasingClip);
    public void FleeingSound() => PlaySound(fleeingClip);
    public void RetreatingSound() => PlaySound(retreatingClip);
    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void StopSound() => audioSource.Stop();

    // Method for playing clip at point
    public void PlaySoundClipAtPoint(ISound sound)
    {
        foreach (var soundClip in sound.SoundClips)
        {
            AudioSource.PlayClipAtPoint(soundClip.clip, soundClip.position, soundClip.volume);
        }
    }
}