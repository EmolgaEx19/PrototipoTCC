using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void GoToRunway()
    {
        //GameManager.Instance.GenerateScore();

        SceneManager.LoadScene(sceneName);
    }
}