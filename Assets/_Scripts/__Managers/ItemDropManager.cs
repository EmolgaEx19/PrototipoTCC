
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    public string LastRewardMessage { get; private set; }

    [Header("Roupas disponíveis para drop")]
    [SerializeField]
    private List<ClothingData> possibleDrops =
        new List<ClothingData>();

    [Header("Recompensa alternativa")]
    [SerializeField] private int fallbackCoins = 50;

    private List<ClothingData> pendingClothes =
        new List<ClothingData>();

    private HashSet<string> reservedIDs =
        new HashSet<string>();

    private int pendingCoins = 0;
    private bool rewardsDelivered = false;

    public bool CollectBox()
    {
        if (rewardsDelivered || GameManager.Instance == null)
            return false;

        List<ClothingData> available =
            new List<ClothingData>();

        foreach (ClothingData clothing in possibleDrops)
        {
            if (clothing == null)
                continue;

            if (string.IsNullOrWhiteSpace(clothing.itemID))
                continue;

            if (reservedIDs.Contains(clothing.itemID))
                continue;

            bool unlocked = GameManager.Instance.IsClothingUnlocked(
                 clothing.itemID,
                 clothing.isLocked
                );

            if (!unlocked)
            {
                available.Add(clothing);
            }
        }

        if (available.Count > 0)
        {
            int index = Random.Range(0, available.Count);

            ClothingData selected = available[index];

            pendingClothes.Add(selected);
            reservedIDs.Add(selected.itemID);

            LastRewardMessage =
              "Nova roupa encontrada: " + selected.clothName;

            /*Debug.Log(
                "Drop reservado: " + selected.clothName
            );*/
        }
        else
        {
            pendingCoins += fallbackCoins;

            LastRewardMessage =
              "Recompensa encontrada: +" + fallbackCoins + " moedas!";

            /*Debug.Log(
                "Recompensa alternativa: " +
                fallbackCoins + " moedas"
            );*/
        }

        return true;
    }

    public int DeliverRewards()
    {
        if (rewardsDelivered)
            return 0;

        rewardsDelivered = true;

        foreach (ClothingData clothing in pendingClothes)
        {
            PlayerPrefs.SetInt(clothing.itemID, 1);

            /*Debug.Log(
                "Roupa desbloqueada: " +
                clothing.clothName
            );*/
        }

        if (pendingClothes.Count > 0)
        {
            PlayerPrefs.Save();
        }

        /*Debug.Log(
            "Moedas alternativas recebidas: " +
            pendingCoins
        );*/

        return pendingCoins;
    }

    public List<ClothingData> GetPendingClothes()
    {
        return new List<ClothingData>(pendingClothes);
    }
}
