
using UnityEngine;

public class CategoryManager : MonoBehaviour
{
    [Header("Painéis das categorias")]
    public GameObject panelCorpo;
    public GameObject panelSapato;
    //public GameObject panelCabelo;

    private void Start()
    {
        ShowCorpo();
    }

    public void ShowCorpo()
    {
        HideAll();
        panelCorpo.SetActive(true);
    }

    public void ShowSapato()
    {
        HideAll();
        panelSapato.SetActive(true);
    }

    /*public void ShowCabelo()
    {
        HideAll();
        panelCabelo.SetActive(true);
    }*/

    private void HideAll()
    {
        panelCorpo.SetActive(false);
        panelSapato.SetActive(false);
        //panelCabelo.SetActive(false);
    }
}