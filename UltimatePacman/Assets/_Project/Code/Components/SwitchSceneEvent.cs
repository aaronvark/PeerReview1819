using UnityEngine;

public class SwitchSceneEvent : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneHandler.Instance.LoadScene(name);
    }

    public void LoadNextScene()
    {
        SceneHandler.Instance.LoadNextScene();
    }
}
