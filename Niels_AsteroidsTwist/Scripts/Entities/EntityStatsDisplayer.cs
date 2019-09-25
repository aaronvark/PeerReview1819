using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityStatsDisplayer : MonoBehaviour
{
    private TextMeshProUGUI textArea;

    [SerializeField]
    private Entity thisEntity;

    private void Start()
    {
        textArea = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textArea.text = thisEntity?.GetHealth.ToString();
    }
}
