
using UnityEngine;
using TMPro;

public class WardrobeUIManager : MonoBehaviour
{
    private GameManager subscribedManager;

    [Header("Atributos gerais")]
    [SerializeField] private TMP_Text generalStatsText;
    [SerializeField] private TMP_Text averageMatchText;

    [Header("Atributos da roupa")]
    [SerializeField] private TMP_Text clothingStatsText;
    [SerializeField] private TMP_Text clothingMatchText;

    [Header("Moedas")]
    [SerializeField] private TMP_Text coinsText;

    private ClothingType currentCategory = ClothingType.Camiseta;

    private void Start()
    {

        if (GameManager.Instance == null)
            return;

        subscribedManager = GameManager.Instance;

        subscribedManager.OnOutfitChanged += UpdateStats;
        subscribedManager.OnCoinsChanged += UpdateCoins;

        UpdateStats();
        UpdateCoins();;
    }

    private void OnDisable()
    {
        if (subscribedManager == null)
            return;

        subscribedManager.OnOutfitChanged -= UpdateStats;
        subscribedManager.OnCoinsChanged -= UpdateCoins;

        subscribedManager = null;
    }

    public void SelectCategory(ClothingType category)
    {
        currentCategory = category;
        UpdateClothingStats();
    }

    public void UpdateCoins()
    {
        coinsText.text =
            $"{GameManager.Instance.GetTotalCoins().ToString()}$";
    }

    public void UpdateStats()
    {
        //Debug.Log("Atualizando atributos...");
        
        OutfitStats stats =
            GameManager.Instance.GetTotalOutfitStats();

        /*Debug.Log(
            $"Velocidade: {stats.speed} | " +
            $"Sorte: {stats.luck} | " +
            $"Vida: {stats.health} | " +
            $"Combinação: {stats.averageThemeMatch}%"
        );*/

        averageMatchText.text =
            $"Combinação média: {stats.averageThemeMatch:0.#}%";

        generalStatsText.text =
            $"Velocidade: {stats.speed:0.##}\n" +
            $"Sorte: {stats.luck:0.##}\n" +
            $"Vida: {stats.health:0.##}";  

        UpdateClothingStats();
    }

    private void UpdateClothingStats()
    {
        ClothingData clothing =
            GameManager.Instance.GetEquippedClothing(currentCategory);

        if (clothing == null)
        {
            clothingStatsText.text = "Nenhuma roupa equipada";
            clothingMatchText.text = "Combinação: 0%";
            return;
        }

        float match =
            GameManager.Instance.GetClothingMatch(currentCategory);

        clothingMatchText.text =
            $"Combinação: {match:0.#}%";

        clothingStatsText.text =
            $"{clothing.clothName}\n" +
            $"Velocidade: {clothing.speedBonus:0.##}\n" +
            $"Sorte: {clothing.luckBonus:0.##}\n" +
            $"Vida: {clothing.healthBonus:0.##}";       
    }
}
