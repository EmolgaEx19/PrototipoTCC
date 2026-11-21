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
    public string itemID;
    public ClothingType type;
    public ClothingRarity rarity;

    [Header("Desbloqueio")]
    [Min(0)] public int price;
    public bool isLocked;

    [Header("Parte vis�vel")]
    public Sprite icon;
    public Sprite characterSprite;


    [Header("Atributos")]
    public float speedBonus;
    public float luckBonus;
    public float healthBonus;

    [Header("B�nus por tema:")]
    public ThemeBonus[] themeBonuses;
}
