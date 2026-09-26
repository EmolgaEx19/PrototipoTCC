
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonVeryfier : MonoBehaviour
{
    [Header("Identificação")]
    public string itemID;

    [Header("Requisito")]
    public int requiredScore;

    [Header("Visual")]
    public Button button;
    public GameObject lockIcon;
    public TMP_Text lockText;

    private bool unlocked;

    private void Start()
    {
        UpdateLockState();
    }

    public void UpdateLockState()
    {
        unlocked = GameManager.Instance.IsClothingUnlocked(itemID);

        if (!unlocked && GameManager.Instance.playerScore >= requiredScore)
        {
            GameManager.Instance.TryUnlockClothing(itemID, requiredScore);

            unlocked = true;
        }

        button.interactable = unlocked;

        lockIcon.SetActive(!unlocked);

        if (lockText != null)
        {
            lockText.gameObject.SetActive(!unlocked);

            if (!unlocked)
            {
                lockText.text = requiredScore + " PONTOS";
            }
        }
    }
}