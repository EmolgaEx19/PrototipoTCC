using UnityEngine;

public enum ClothingType
{
    Camiseta,
    Calca,
    Tenis,
    Acessorio
}

public enum ClothingRarity
{
    Comum,
    Rara,
    Epica,
    Lendaria
}

[CreateAssetMenu(fileName = "NewClothing",
    menuName = "DripLab/Clothing")]

public class ClothingData : ScriptableObject
{
    [Header("Informacoes")]
    public string clothName;
    public ClothingType type;
    public ClothingRarity rarity;

    [Header("Parte visível")]
    public Sprite icon;
    public Sprite characterSprite;

    /*[TextArea]
    public string description;*/

    /*[Header("Pontuacao")]
    public int baseScore;*/

    [Header("Atributos")]
    public float speedBonus;
    public float luckBonus;
    public float healthBonus;

    [Header("Bônus por tema:")]
    public ThemeBonus[] themeBonuses;
}
