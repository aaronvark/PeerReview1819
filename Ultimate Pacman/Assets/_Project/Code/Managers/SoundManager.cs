using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public void PlaySoundClipAtPoint(ISound sound)
    {
        foreach (var soundClip in sound.SoundClips)
        {
            AudioSource.PlayClipAtPoint(soundClip.clip, soundClip.position, soundClip.volume);
        }
    }
}