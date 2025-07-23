using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorUI : MonoBehaviour
{
    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void StartAutoMode()
    {
        SceneManager.LoadScene(2);
    }
}
