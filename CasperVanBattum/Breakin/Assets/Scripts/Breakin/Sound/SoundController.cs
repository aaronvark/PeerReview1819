using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Breakin.Sound
{
    public class SoundController : ISFXController
    {
        private static ISFXController instance;
        public static ISFXController Instance => instance ?? (instance = new SoundController());
        
        public event Action SoundPlayed;

        private readonly GameObject soundPlayer;
        private readonly AudioSource audio;

        private AudioClip[] clips;
        
        public SoundController()
        {
            // TODO maybe add a tag and check against that tag to ensure there is only a single sound player in the game
            // Create new game object that will play sounds
            soundPlayer = new GameObject("_SoundPlayer");
            
            // Init audio source component
            audio = soundPlayer.AddComponent<AudioSource>();
            audio.loop = false;
            
            // Load clips
            clips = Resources.LoadAll<AudioClip>("Audio/SFX");
        }

        public void PlaySound(int index)
        {
            PlaySound(index, Vector3.zero);
        }

        public void PlaySound(int index, Vector3 source)
        {
            soundPlayer.transform.position = source;
            audio.PlayOneShot(clips[index]);
            
            SoundPlayed?.Invoke();
        }
    }
}