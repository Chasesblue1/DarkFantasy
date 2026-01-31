using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLoader : MonoBehaviour
{
    public string nextSceneName = "Scene1";

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

