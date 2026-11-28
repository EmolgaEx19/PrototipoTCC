
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RunnerGameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private RunnerUIManager runnerUI;
    [SerializeField] private ItemDropUI itemDropUI;
    [SerializeField] private ItemDropManager itemDropManager;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalDistanceText;
    [SerializeField] private TMP_Text finalCoinsText;
    [SerializeField] private TMP_Text multiplierText;
    [SerializeField] private TMP_Text boxCoinsText;
    [SerializeField] private TMP_Text totalCoinsText;

    [Header("Reward Settings")]
    [SerializeField] private float coinMultiplier = 1f;

    private bool isGameOver = false;

    private void Start()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
    }

    private float GetCoinMultiplier()
    {
        OutfitStats stats =
            GameManager.Instance.GetTotalOutfitStats();

        float averageMatch = stats.averageThemeMatch;

        if (averageMatch >= 100f)
            return 2f;

        if (averageMatch >= 80f)
            return 1.5f;

        if (averageMatch >= 60f)
            return 1.2f;

        if (averageMatch >= 40f)
            return 1.1f;

        return 1f;
    }

    public void FinalizeRun()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        int distance = scoreManager.GetScore();
        int collectedCoins = runnerUI.GetRunCoins();
        float coinMultiplier = GetCoinMultiplier();

        int rewardedCoins = Mathf.FloorToInt(
            collectedCoins * coinMultiplier
        );

        int itemBoxCoins = 0;

        if (itemDropManager != null)
        {
            itemBoxCoins = itemDropManager.DeliverRewards();
            //Debug.Log("Moedas concedidas: " +  itemBoxCoins);
        }

        int finalReward = rewardedCoins + itemBoxCoins; //total de moedas após multiplicação + moedas das caixas


        // Transfere as moedas somente ao finalizar a partida
        GameManager.Instance.AddCoin(finalReward);

        // Atualiza os textos do Game Over
        finalDistanceText.text = distance + " m";

        finalCoinsText.text = $"{collectedCoins}$";

        multiplierText.text = "x" + coinMultiplier.ToString("0.##");

        if (itemBoxCoins > 0)
        {
            boxCoinsText.text = $"+{finalReward - collectedCoins}";
        }

        totalCoinsText.text = $"{finalReward.ToString()}$";

        if (itemDropUI != null)
        {
            itemDropUI.PrepareGameOver();
        }

        // Exibe o menu e pausa o jogo
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartRun()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void GameOverChangeScene(string sceneName)
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetOutfit();
        }

        SceneManager.LoadScene(sceneName);
    }
}
