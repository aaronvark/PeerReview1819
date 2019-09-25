using Breakin.GameManagement;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Breakin.UI
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] private Text text;

        private void Start()
        {
            EventManager.displayMessage += Show;
            EventManager.hideMessage += Hide;

            // Try to find a text component 
            if (!text) LoadTextComponent();

            // Start deactivated when the game starts
            gameObject.SetActive(false);
        }

        private void Reset()
        {
            LoadTextComponent();
        }

        private void OnDestroy()
        {
            EventManager.hideMessage -= Hide;
            EventManager.displayMessage -= Show;
        }

        private void LoadTextComponent()
        {
            // The text component may either be on the gameobject itself, or on any of its children
            text = GetComponentInChildren<Text>();
        }

        private void Show(string msg)
        {
            Assert.IsNotNull(text);

            gameObject.SetActive(true);
            text.text = msg;
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}