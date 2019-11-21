using UnityEngine;

public struct SoundClip
{
    public AudioClip clip;
    public Vector3 position;
    public float volume;

    public SoundClip(AudioClip clip)
    {
        this.clip = clip;
        position = Vector3.zero;
        volume = 1f;
    }

    public SoundClip(AudioClip clip, Vector3 position) : this(clip)
    {
        this.position = position;
    }

    public SoundClip(AudioClip clip, float volume) : this(clip)
    {
        this.volume = volume;
    }

    public SoundClip(AudioClip clip, Vector3 position, float volume) : this(clip, position)
    {
        this.volume = volume;
    }
}
