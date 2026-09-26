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

    private void Start()
    {
       //ResetRoupaEspecial();
    }

    public void GenerateScore()
    {
        playerScore = Random.Range(60, 101);
        Debug.Log("Pontuação do desfile: " + playerScore);
    }

    public void TryUnlockClothing(string itemID, int requiredScore)
    {
        if (playerScore >= requiredScore)
        {
            PlayerPrefs.SetInt(itemID, 1);
            PlayerPrefs.Save();

            Debug.Log(itemID + " foi desbloqueada!");
        }
    }

    /*public void CheckClothingUnlocks()
    {
        if (playerScore >= 75)
        {
            PlayerPrefs.SetInt("Especial", 1);
        }

        if (playerScore >= 90)
        {
            PlayerPrefs.SetInt("Exclusiva", 1);
        }

        PlayerPrefs.Save();
    }*/

    public bool IsClothingUnlocked(string itemID)
    {
        int savedValue = PlayerPrefs.GetInt(itemID, 0);

        Debug.Log("Item: " + itemID +
              " | Valor salvo: " + savedValue);

        // Se não houver registro, o item começa bloqueado.
        return savedValue == 1;
    }

    //Exclui os valores salvos de desbloqueio
    public void ResetRoupaEspecial()
    {
        PlayerPrefs.DeleteKey("Especial");
        PlayerPrefs.DeleteKey("Exclusiva");
        PlayerPrefs.Save();

        Debug.Log("Desbloqueio da Especial apagado!");
    }

}
