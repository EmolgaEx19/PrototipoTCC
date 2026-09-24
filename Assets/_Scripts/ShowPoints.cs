using UnityEngine;
using TMPro;

public class ShowPoints : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = (GameManager.Instance.playerScore).ToString();
    }
}