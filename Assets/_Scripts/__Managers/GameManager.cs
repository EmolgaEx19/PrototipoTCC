using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Look do jogador")]
    public Sprite selectedBody;
    public Sprite selectedShoes;

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

    //colocar a lógica das moedas/desbloqueio de roupas em outro script?


    public void AddCoin(int amount)
    {
        if (amount <= 0) return;
        
        totalCoins += amount;

        PlayerPrefs.SetInt(COINS_KEY, totalCoins);
        PlayerPrefs.Save();

        Debug.Log($"Total de moedas: {totalCoins}");
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }

    // Tenta gastar moedas
    public bool TrySpendCoins(int amount)
    {
        if (amount < 0 || totalCoins < amount)
            return false;

        totalCoins -= amount;

        PlayerPrefs.SetInt(COINS_KEY, totalCoins);
        PlayerPrefs.Save();

        Debug.Log($"Moedas restantes: {totalCoins}");

        return true;
    }

    // Verifica se a roupa está desbloqueada
    public bool IsClothingUnlocked(
        string itemID, bool initiallyLocked)
    {
        if (string.IsNullOrWhiteSpace(itemID))
            return false;

        // Roupas inicialmente desbloqueadas
        if (!initiallyLocked)
            return true;

        return PlayerPrefs.GetInt(itemID, 0) == 1;
    }

    // Tenta comprar uma roupa
    public bool TryUnlockClothing(string itemID, int price)
    {
        if (string.IsNullOrWhiteSpace(itemID) || price < 0)
            return false;

        // Verifica se a roupa já foi comprada
        if (PlayerPrefs.GetInt(itemID, 0) == 1)
            return true;

        // Verifica se possui moedas suficientes
        if (!TrySpendCoins(price))
        {
            Debug.Log("Moedas insuficientes!");
            return false;
        }

        // Salva o desbloqueio
        PlayerPrefs.SetInt(itemID, 1);
        PlayerPrefs.Save();

        Debug.Log($"Roupa {itemID} desbloqueada!");

        return true;
    }
}
