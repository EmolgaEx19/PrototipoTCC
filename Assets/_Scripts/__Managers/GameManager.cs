using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Look do jogador")]
    public Sprite selectedBody;
    public Sprite selectedShoes;

    [Header("Pontuação")]
    public int playerScore;

    [Header("Coins Settings")]
    [SerializeField] private int totalCoins;


    private const string COINS_KEY = "PlayerCoins";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            totalCoins =  PlayerPrefs.GetInt(COINS_KEY, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //Gera o score aleatório
    // -> Apagar depois
    public void GenerateScore()
    {
        playerScore = Random.Range(60, 101);
    }

    public void TryUnlockClothing(string itemID, int requiredScore)
    {
        if (playerScore >= requiredScore)
        {
            PlayerPrefs.SetInt(itemID, 1);
            PlayerPrefs.Save();

        }
    }

    public bool IsClothingUnlocked(string itemID)
    {
        int savedValue = PlayerPrefs.GetInt(itemID, 0);

        return savedValue == 1;
    }

    //Fazer a função pra remover tbm
    public void AddCoin(int amount)
    {
        totalCoins += amount;

        PlayerPrefs.SetInt(COINS_KEY, totalCoins);
        PlayerPrefs.Save();

        Debug.Log($"Total de moedas: {totalCoins}");
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }
}
