using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace WorldBuilderTool
{
    public class RuntimeUI : MonoBehaviour
    {
        EasyWorldBuilder mgr;

        VerticalLayoutGroup scrollBar;
        [SerializeField] List<RuntimeButton> buttons = new List<RuntimeButton>();

        private void Start()
        {
            mgr = FindObjectOfType<EasyWorldBuilder>();

            scrollBar = GetComponentInChildren<VerticalLayoutGroup>();

            //Draw initial UI buttons.
            for (int i = 0; i < mgr.PlaceableAssets.Count; i++)
            {
                addButton(mgr.PlaceableAssets[i]);
            }
        }

        private void Update()
        {
            for (int i = 0; i < mgr.PlaceableAssets.Count; i++)
            {
                if (buttons[i] != null)
                {
                    buttons[i].GetComponent<RuntimeButton>().Asset = mgr.PlaceableAssets[i];
                }
            }
        }

        public void addButton(GameObject asset)
        {
            Button button = Instantiate(Resources.Load<GameObject>("EasyWorldBuilder/WorldBuilder_Button").GetComponent<Button>());
            button.transform.SetParent(scrollBar.transform);

            buttons.Add(button.GetComponent<RuntimeButton>());

            if (asset != null)
            {
                button.GetComponent<RuntimeButton>().Asset = asset;
            }  
        }

        public void removeButton()
        {
            Destroy(buttons[buttons.Count - 1].gameObject);
            buttons.RemoveAt(buttons.Count - 1);
        }
    }
}
