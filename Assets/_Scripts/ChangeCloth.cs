using UnityEngine;

public class ChangeCloth : MonoBehaviour
{
    [Header("Partes da personagem")]
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer shoesRenderer;

    public void ChangeBody(Sprite newBody)
    {
        bodyRenderer.sprite = newBody;
    }

    public void ChangeShoes(Sprite newShoes)
    {
        shoesRenderer.sprite = newShoes;
    }

    public void ClearBody()
    {
        bodyRenderer.sprite = null;
    }

    public void ClearShoes()
    {
        shoesRenderer.sprite = null;
    }
}