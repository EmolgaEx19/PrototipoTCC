using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneAndReset(string sceneName)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetOutfit();
        }

        SceneManager.LoadScene(sceneName);
    }
}