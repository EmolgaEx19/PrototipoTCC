using UnityEngine;

public class ChangeCloth : MonoBehaviour
{
    [Header("Partes da personagem")]
    public SpriteRenderer shirtRenderer;
    public SpriteRenderer shoesRenderer;


    public void ChangeShirt(Sprite newShirt)
    {
        shirtRenderer.sprite = newShirt;

        GameManager.Instance.selectedBody = newShirt;
    }

    public void ChangeShoes(Sprite newShoes)
    {
        shoesRenderer.sprite = newShoes;

        GameManager.Instance.selectedShoes = newShoes;
    }  
}