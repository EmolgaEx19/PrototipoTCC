using UnityEngine;

[CreateAssetMenu(fileName = "NewTheme", menuName = "DripLab/Theme")]
public class RoundTHemes : ScriptableObject
{
    public string themeName;

    public string[] preferredTags;

    public float bonusPerMatchingTag = 20f;
}