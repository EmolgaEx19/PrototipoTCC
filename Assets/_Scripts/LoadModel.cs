using UnityEngine;

public class LoadModel : MonoBehaviour
{
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer shoesRenderer;

    private void Start()
    {
        LoadPlayerLook();
    }

    private void LoadPlayerLook()
    {
        bodyRenderer.sprite = GameManager.Instance.selectedBody;
        shoesRenderer.sprite = GameManager.Instance.selectedShoes;
    }
}