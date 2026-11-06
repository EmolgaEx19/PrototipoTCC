using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    //private float vel = 5f;

    /*private void Update()
    {
        transform.position += Vector3.left * vel * Time.deltaTime;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddCoin(coinValue);

            Destroy(gameObject);
        }
    }
}
