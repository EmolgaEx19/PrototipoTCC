
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    [Header("Roupas disponíveis para drop")]
    [SerializeField]
    private List<ClothingDropEntry> possibleDrops =
        new List<ClothingDropEntry>();

    [Header("Recompensa alternativa")]
    [SerializeField] private int fallbackCoins = 50;

    private List<ClothingDropEntry> pendingClothes =
        new List<ClothingDropEntry>();

    private HashSet<string> reservedIDs =
        new HashSet<string>();

    private int pendingCoins = 0;
    private bool rewardsDelivered = false;

    public bool CollectBox()
    {
        if (rewardsDelivered || GameManager.Instance == null)
            return false;

        List<ClothingDropEntry> available =
            new List<ClothingDropEntry>();

        foreach (ClothingDropEntry entry in possibleDrops)
        {
            if (entry == null || entry.clothing == null)
                continue;

            if (string.IsNullOrWhiteSpace(entry.itemID))
                continue;

            if (reservedIDs.Contains(entry.itemID))
                continue;

            bool unlocked = GameManager.Instance.IsClothingUnlocked(
                 clothing.itemID,
                 clothing.isLocked
                );

            if (!unlocked)
            {
                available.Add(entry);
            }
        }

        if (available.Count > 0)
        {
            int index = Random.Range(0, available.Count);

            ClothingDropEntry selected = available[index];

            pendingClothes.Add(selected);
            reservedIDs.Add(selected.itemID);

            Debug.Log(
                "Drop reservado: " + selected.clothing.clothName
            );
        }
        else
        {
            pendingCoins += Mathf.Max(0, fallbackCoins);

            Debug.Log(
                "Recompensa alternativa: " +
                fallbackCoins + " moedas"
            );
        }

        return true;
    }

    public int DeliverRewards()
    {
        if (rewardsDelivered)
            return 0;

        rewardsDelivered = true;

        foreach (ClothingDropEntry entry in pendingClothes)
        {
            PlayerPrefs.SetInt(entry.itemID, 1);

            Debug.Log(
                "Roupa desbloqueada: " +
                entry.clothing.clothName
            );
        }

        if (pendingClothes.Count > 0)
        {
            PlayerPrefs.Save();
        }

        Debug.Log(
            "Moedas alternativas recebidas: " +
            pendingCoins
        );

        return pendingCoins;
    }

    public List<ClothingData> GetPendingClothes()
    {
        List<ClothingData> result =
            new List<ClothingData>();

        foreach (ClothingDropEntry entry in pendingClothes)
        {
            result.Add(entry.clothing);
        }

        return result;
    }
}
