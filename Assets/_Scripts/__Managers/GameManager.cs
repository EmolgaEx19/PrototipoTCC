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
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GenerateScore()
    {
        playerScore = Random.Range(60, 101);
        CheckClothingUnlocks();
    }

    
    public void CheckClothingUnlocks()
    {
        if (playerScore >= 80)
        {
            PlayerPrefs.SetInt("RoupaEspecial", 1);
        }

        if (playerScore >= 90)
        {
            PlayerPrefs.SetInt("RoupaExclusiva", 1);
        }

        PlayerPrefs.Save();
    }

    public bool IsClothingUnlocked(string itemID)
    {
        // Se não houver registro, o item começa bloqueado.
        return PlayerPrefs.GetInt(itemID, 0) == 1;
    }

}
