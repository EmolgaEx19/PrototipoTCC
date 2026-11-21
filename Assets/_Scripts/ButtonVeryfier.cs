
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ButtonVeryfier : MonoBehaviour
{
    [Header("Identificação")]
    //public string itemID;
    public ClothingData clothingData;

    [Header("Visual")]
    public Button button;
    public GameObject lockIcon;
    public TMP_Text lockText;

    public UnityEvent onClothingSelected;

    private bool unlocked;

    private void Start()
    {
        button.onClick.AddListener(OnClothingClick);

        UpdateLockState();
    }

    private void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveListener(OnClothingClick);
    }

    public void UpdateLockState()
    {
        if (GameManager.Instance == null || clothingData == null)
            return;

        unlocked = GameManager.Instance.IsClothingUnlocked(
            clothingData.itemID,
            clothingData.isLocked
        );

        // O botão precisa continuar clicável para permitir a compra.
        button.interactable = true;

        if (lockIcon != null)
            lockIcon.SetActive(!unlocked);

        if (lockText != null)
        {
            lockText.gameObject.SetActive(!unlocked);

            if (!unlocked)
                lockText.text = clothingData.price + " MOEDAS";
        }
    }

    public void OnClothingClick()
    {
        if (GameManager.Instance == null || clothingData == null)
            return;

        if (!unlocked)
        {
            bool purchased = GameManager.Instance.TryUnlockClothing(
                clothingData.itemID,
                clothingData.price
            );

            if (!purchased)
            {
                //Debug.Log("Moedas insuficientes!");
                return;
            }

            UpdateLockState();

            //Debug.Log("Roupa comprada: " + clothingData.clothName);
        }

        // Registra a roupa equipada no GameManager
        GameManager.Instance.EquipClothing(clothingData);

        // Executa as ações de seleção
        onClothingSelected?.Invoke();
    }
}