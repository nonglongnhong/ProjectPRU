using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "SampleScene";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A key pressed");
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
