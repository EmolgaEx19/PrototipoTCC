
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
        //verifica se algum tema foi criado
        if (availableThemes == null || availableThemes.Length == 0)
        {
            //Debug.LogWarning("Nenhum tema foi configurado!");
            return;
        }

        // Seleciona apenas temas válidos.
        System.Collections.Generic.List<PossibleTheme> validThemes =
            new System.Collections.Generic.List<PossibleTheme>();

        foreach (PossibleTheme theme in availableThemes)
        {
            if (theme != null)
            {
                validThemes.Add(theme);
            }
        }

        //verifica se algum tema válido foi adicionado
        if (validThemes.Count == 0)
        {
            //Debug.LogWarning("Nenhum tema válido encontrado!");
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

        //Debug.Log("Tema sorteado: " + CurrentTheme.themeName);
    }
}
