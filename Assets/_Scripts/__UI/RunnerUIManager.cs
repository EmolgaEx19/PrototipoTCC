
using UnityEngine;
using TMPro;

public class RunnerUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text coinsText;

    [Header("Managers")]
    [SerializeField] private ScoreManager scoreManager;

    private int runCoins = 0;

    private void Start()
    {
        UpdateDistanceText();
        UpdateCoinsText();
    }

    private void Update()
    {
        UpdateDistanceText();
    }

    private void UpdateDistanceText()
    {
        if (scoreManager == null)
            return;

        distanceText.text =
            "Distância: " + scoreManager.GetScore() + " m";
    }

    private void UpdateCoinsText()
    {
        coinsText.text = "Moedas: " + runCoins;
    }

    public void AddRunCoins(int amount)
    {
        if (amount <= 0)
            return;

        runCoins += amount;

        UpdateCoinsText();
    }

    public int GetRunCoins()
    {
        return runCoins;
    }
}
