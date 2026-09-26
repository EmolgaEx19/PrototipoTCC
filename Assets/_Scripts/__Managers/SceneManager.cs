using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [Header("Configuração de Pontuação")]
    [SerializeField] private bool generateScoreOnTransition = false;


    public void ChangeScene()
    {
        if (generateScoreOnTransition)
        {
            GameManager.Instance.GenerateScore();
        }

        SceneManager.LoadScene(sceneName);
    }
}