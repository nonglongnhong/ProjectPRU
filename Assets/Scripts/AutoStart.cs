using UnityEngine;
using UnityEngine.SceneManagement;

public static class AutoStart
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        Application.runInBackground = true;
        Object.DontDestroyOnLoad(new GameObject("InputStarter", typeof(InputStarter)));
    }

    private class InputStarter : MonoBehaviour
    {
        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu" && Input.anyKeyDown)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
