using UnityEngine;
using System.Collections;

public class DeletePrefs : MonoBehaviour
{
    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private float notificationDuration = 3f;

    public void DeletePlayerData()
    {
        //PlayerPrefs.DeleteKey("");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        StartCoroutine(NotificationRoutine());
    }

    private IEnumerator NotificationRoutine()
    {
        notificationPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(
            notificationDuration
        );

        notificationPanel.SetActive(false);
        //notificationCoroutine = null;
    }
}
