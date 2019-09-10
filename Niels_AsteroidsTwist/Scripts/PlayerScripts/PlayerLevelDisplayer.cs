using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerLevelDisplayer : MonoBehaviour
{
    private TextMeshProUGUI textArea;

    [SerializeField]
    private PlayerLeveler thisEntity;

    private void Start()
    {
        textArea = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textArea.text = thisEntity?.GetScrap.ToString();
    }
}
