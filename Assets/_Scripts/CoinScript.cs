using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    //[SerializeField] private float coinSpeed = 5f;

    private void Update()
    {
        transform.position += Vector3.left * GroundMovement.Instance.GetSpeed() * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddCoin(coinValue);

            Destroy(gameObject);
        }
    }
}
