using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WorldBuilderTool
{
    public class RuntimeButton : MonoBehaviour
    {
        public GameObject Asset;
        

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => ChooseAsset());
        }

        private void Update()
        {
            if (Asset != null)
            {
                GetComponentInChildren<Text>().text = Asset.name;
            }
        }

        void ChooseAsset()
        {
            FindObjectOfType<EasyWorldBuilder>().SelectedAsset = Asset;
        }

    }
}
