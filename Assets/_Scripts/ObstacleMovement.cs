using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float obstacleSpeed = 5f;

    [SerializeField] private float destroyX = -15f;


    private void Update()
    {
        // Movimenta o obstáculo para a esquerda
        transform.position += Vector3.left * obstacleSpeed * Time.deltaTime;

        // Destrói o obstáculo quando sair da tela
        if (transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        obstacleSpeed = newSpeed;
    }
}
