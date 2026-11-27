using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    private float obstacleSpeed;
    private bool isDiferent = false;


    private void Update()
    {
        if (!isDiferent)
        {
            obstacleSpeed = GroundMovement.Instance.GetSpeed();
        }

        // Movimenta o obstáculo para a esquerda
        transform.position += Vector3.left * obstacleSpeed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player =
                collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(GetDamage());
            }
        }

        if (collision.gameObject.CompareTag("Destroyer"))
        {            
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        obstacleSpeed = newSpeed;
    }

    private int GetDamage()
    {
        return damage;
    }
}
