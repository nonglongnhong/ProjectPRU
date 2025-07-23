using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "LevelSelector";

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("Any key pressed");
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
