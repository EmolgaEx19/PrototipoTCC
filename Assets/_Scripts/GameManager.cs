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
    }

}
