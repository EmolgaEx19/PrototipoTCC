
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private ItemDropManager dropManager;
    private bool collected = false;

    private void Start()
    {
        dropManager =
            FindFirstObjectByType<ItemDropManager>();
    }

    private void Update()
    {
        if (GroundMovement.Instance == null)
            return;

            transform.position += Vector3.left *
            GroundMovement.Instance.GetSpeed() *
            Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
            return;
        }

        if (collected || !collision.CompareTag("Player"))
            return;

        if (dropManager == null)
            return;

        if (dropManager.CollectBox())
        {
            collected = true;
            Destroy(gameObject);
        }
    }
}
