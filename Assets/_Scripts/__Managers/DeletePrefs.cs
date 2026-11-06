using UnityEngine;

public class DeletePrefs : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.DeleteKey("");
        PlayerPrefs.DeleteAll();
    }
}
