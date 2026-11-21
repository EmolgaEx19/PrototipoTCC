
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlashUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image damagePanel;

    [Header("Flash Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float maxAlpha = 0.55f;

    [SerializeField] private float fadeDuration = 0.2f;

    private Coroutine flashCoroutine;

    private void Awake()
    {
        SetAlpha(0f);
    }

    public void Flash()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        float elapsed = 0f;

        SetAlpha(maxAlpha);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(
                elapsed / fadeDuration
            );

            float alpha = Mathf.Lerp(
                maxAlpha, 0f, progress
            );

            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(0f);
        flashCoroutine = null;
    }

    private void SetAlpha(float alpha)
    {
        Color color = damagePanel.color;
        color.a = alpha;

        damagePanel.color = color;
    }
}
