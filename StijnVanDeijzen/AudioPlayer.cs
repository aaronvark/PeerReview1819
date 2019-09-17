using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    //not yet tested / used

    private static AudioPlayer instance;
    public static AudioPlayer Instance { get { return instance; } }
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this.gameObject); }
        else { instance = this; }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip,float volume)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }
}
