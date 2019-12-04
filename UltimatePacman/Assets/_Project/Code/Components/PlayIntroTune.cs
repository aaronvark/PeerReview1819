using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayIntroTune : MonoBehaviour
{
    private IEnumerator Start()
    {
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(GetComponent<AudioSource>().clip.length);
        yield return new WaitForSecondsRealtime(1);

        Time.timeScale = 1;
        SoundManager.Instance.ChasingSound();
        Destroy(gameObject);
    }
}
