using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Is responsible of the in game ui.
/// </summary>
public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private Text waveText;

    private void OnEnable()
    {
        WaveManager.onWaveStart += SetWave;
    }
    private void OnDisable()
    {
        WaveManager.onWaveStart -= SetWave;
    }

    public void SetWave(int _val)
    {
        waveText.text = "wave " + _val;
    }
}
