
using UnityEngine;
using TMPro;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance;

    [Header("Temas disponíveis")]
    [SerializeField] private PossibleTheme[] availableThemes;

    [Header("Interface")]
    [SerializeField] private TMP_Text themeText;

    public PossibleTheme CurrentTheme { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateRandomTheme();
    }

    public void GenerateRandomTheme()
    {
        if (availableThemes == null || availableThemes.Length == 0)
        {
            return;
        }

        System.Collections.Generic.List<PossibleTheme> validThemes =
            new System.Collections.Generic.List<PossibleTheme>();

        foreach (PossibleTheme theme in availableThemes)
        {
            if (theme != null)
            {
                validThemes.Add(theme);
            }
        }

        if (validThemes.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, validThemes.Count);

        CurrentTheme = validThemes[randomIndex];

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentTheme(CurrentTheme);
        }

        if (themeText != null)
        {
            themeText.text = CurrentTheme.themeName;
        }
    }
}
