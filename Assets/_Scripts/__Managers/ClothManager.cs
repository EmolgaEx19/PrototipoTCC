using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothManager : MonoBehaviour
{
    //public GameManager gameManager;
    [SerializeField] private Sprite baseBody;
    [SerializeField] private Sprite baseShoes;

    public void VerifyCloth()
    {
        if(GameManager.Instance.selectedBody == null)
        {
            GameManager.Instance.selectedBody = baseBody;
        }

        if (GameManager.Instance.selectedShoes == null)
        {
            GameManager.Instance.selectedShoes = baseShoes;
        }
    }

    public void ResetBody()
    {
        /*GameManager.Instance.selectedBody = baseBody;
        GameManager.Instance.selectedShoes = baseShoes;*/
    }
}

