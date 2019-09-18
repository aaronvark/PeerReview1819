using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHandeler : MonoBehaviour
{


    public void GoToScene(int _index)
    {
        SceneManager.LoadScene(_index);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
