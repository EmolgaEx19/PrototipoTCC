
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [Header("Painel a ser habilitado")]
    [SerializeField] private GameObject selectedPanel;

    public void OpenPanel()
    {
        selectedPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        selectedPanel.SetActive(false);
    }
}
