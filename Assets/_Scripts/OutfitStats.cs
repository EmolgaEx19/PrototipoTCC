
using System.Collections.Generic;
using UnityEngine;

public class OutfitStats
{
    public float speed;
    public float luck;
    public float health;
    public float averageThemeMatch;

    public static float GetThemeMatch(
        ClothingData clothing,
        PossibleTheme currentTheme)
    {
        if (clothing == null || currentTheme == null)
            return 0f;

        if (clothing.themeBonuses == null)
            return 0f;

        foreach (ThemeBonus bonus in clothing.themeBonuses)
        {
            if (bonus != null && bonus.theme == currentTheme)
            {
                // Converte os níveis 1-5 para 20%-100%.
                return bonus.matchLevel * 20f;
            }
        }

        return 0f;
    }

    public static OutfitStats Calculate(
        Dictionary<ClothingType, ClothingData> equipped,
        PossibleTheme currentTheme)
    {
        OutfitStats result = new OutfitStats();

        float totalMatch = 0f;
        int clothingCount = 0;

        foreach (ClothingData clothing in equipped.Values)
        {
            if (clothing == null)
                continue;

            result.speed += clothing.speedBonus;
            result.luck += clothing.luckBonus;
            result.health += clothing.healthBonus;

            totalMatch += GetThemeMatch(clothing, currentTheme);
            clothingCount++;
        }

        if (clothingCount > 0)
        {
            result.averageThemeMatch =
                totalMatch / clothingCount;
        }

        return result;
    }
}
