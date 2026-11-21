
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDropUI : MonoBehaviour
{
    [Header("Notificação da caixa")]
    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private float notificationDuration = 2f;

    [Header("Menu de recompensas")]
    [SerializeField] private GameObject showRewardsButton;
    [SerializeField] private GameObject rewardsPanel;
    [SerializeField] private TMP_Text rewardsText;

    [Header("Managers")]
    [SerializeField] private ItemDropManager itemDropManager;

    private Coroutine notificationCoroutine;

    private void Start()
    {
        notificationPanel.SetActive(false);
        showRewardsButton.SetActive(false);
        rewardsPanel.SetActive(false);
    }

    public void ShowNotification(string message)
    {
        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }

        notificationCoroutine =
            StartCoroutine(NotificationRoutine(message));
    }

    private IEnumerator NotificationRoutine(string message)
    {
        notificationText.text = message;
        notificationPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(
            notificationDuration
        );

        notificationPanel.SetActive(false);
        notificationCoroutine = null;
    }

    public void PrepareGameOver()
    {
        List<ClothingData> clothes =
            itemDropManager.GetPendingClothes();

        bool hasNewClothes = clothes.Count > 0;

        showRewardsButton.SetActive(hasNewClothes);
        rewardsPanel.SetActive(false);

        if (!hasNewClothes)
            return;

        rewardsText.text = "";

        foreach (ClothingData clothing in clothes)
        {
            rewardsText.text +=
                "• " + clothing.clothName + "\n";
        }
    }

    public void OpenRewardsPanel()
    {
        rewardsPanel.SetActive(true);
    }

    public void CloseRewardsPanel()
    {
        rewardsPanel.SetActive(false);
    }
}