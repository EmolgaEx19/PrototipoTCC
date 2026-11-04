using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private GroundMovement gmov;
    private float obstacleSpeed = 5f;
    [SerializeField] private int damage = 25;

    private void Awake()
    {
        gmov = FindFirstObjectByType<GroundMovement>();
    }

    private void Update()
    {
        // Movimenta o obstáculo para a esquerda
        transform.position += Vector3.left * gmov.groundSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyer"))
        {            
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        obstacleSpeed = newSpeed;
    }

    public int GetDamage()
    {
        return damage;
    }
}
