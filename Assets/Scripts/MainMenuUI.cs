using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "SampleScene";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
