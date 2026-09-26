using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Look do jogador")]
    public Sprite selectedBody;
    public Sprite selectedShoes;

    [Header("Pontua��o")]
    public int playerScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
}
