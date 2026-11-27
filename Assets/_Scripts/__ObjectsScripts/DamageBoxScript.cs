
using UnityEngine;

public class BoxDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private int damage = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerController player =
            collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}