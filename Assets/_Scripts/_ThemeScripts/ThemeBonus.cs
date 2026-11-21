using System;
using UnityEngine;

[Serializable]
public class ThemeBonus
{
    public PossibleTheme theme;

    [Header("Nível de combinação")]
    [Range(1,5)]
    public int matchLevel;
}
