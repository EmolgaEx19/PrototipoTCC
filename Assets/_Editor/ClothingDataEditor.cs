
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ClothingData))]
public class ClothingDataEditor : Editor
{
    private void OnEnable()
    {
        SyncThemes();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Atualizar lista de temas"))
        {
            SyncThemes();
        }
    }

    private void SyncThemes()
    {
        ClothingData item = (ClothingData)target;

        string[] guids = AssetDatabase.FindAssets("t:RoundTHemes");

        List<RoundTHemes> themes = new List<RoundTHemes>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            RoundTHemes theme =
                AssetDatabase.LoadAssetAtPath<RoundTHemes>(path);

            if (theme != null)
                themes.Add(theme);
        }

        List<ThemeBonus> updatedBonuses = new List<ThemeBonus>();

        foreach (RoundTHemes theme in themes)
        {
            ThemeBonus existing = null;

            if (item.themeBonuses != null)
            {
                foreach (ThemeBonus bonus in item.themeBonuses)
                {
                    if (bonus != null && bonus.theme == theme)
                    {
                        existing = bonus;
                        break;
                    }
                }
            }

            if (existing != null)
            {
                updatedBonuses.Add(existing);
            }
            else
            {
                updatedBonuses.Add(new ThemeBonus
                {
                    theme = theme,
                    bonus = 0
                });
            }
        }

        if (item.themeBonuses == null ||
            item.themeBonuses.Length != updatedBonuses.Count)
        {
            Undo.RecordObject(item, "Atualizar temas da roupa");

            item.themeBonuses = updatedBonuses.ToArray();

            EditorUtility.SetDirty(item);
        }
        else
        {
            // Atualiza caso os temas tenham mudado de ordem.
            bool changed = false;

            for (int i = 0; i < updatedBonuses.Count; i++)
            {
                if (item.themeBonuses[i].theme != updatedBonuses[i].theme)
                {
                    changed = true;
                    break;
                }
            }

            if (changed)
            {
                Undo.RecordObject(item, "Atualizar temas da roupa");

                item.themeBonuses = updatedBonuses.ToArray();

                EditorUtility.SetDirty(item);
            }
        }
    }
}
