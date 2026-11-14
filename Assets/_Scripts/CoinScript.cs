using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    //[SerializeField] private float coinSpeed = 5f;

    private RunnerUIManager runnerUI;

    private void Start()
    {
        runnerUI = FindFirstObjectByType<RunnerUIManager>();
    }


    private void Update()
    {
        transform.position += Vector3.left * GroundMovement.Instance.GetSpeed() * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.CompareTag("Player"))
            {
                if (runnerUI == null)
                    return;

                runnerUI.AddRunCoins(coinValue);

                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
