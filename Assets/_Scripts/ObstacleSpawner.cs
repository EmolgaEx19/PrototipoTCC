
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 4f;

    [SerializeField] private float obstacleSpeed = 5f;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            ResetSpawnTimer();
            SpawnObstacle();  
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefabs.Length == 0)
            return;

        // Escolhe um obstáculo aleatório
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);

        GameObject selectedObstacle = obstaclePrefabs[randomIndex];

        // Cria o obstáculo na posição definida
        GameObject newObstacle = Instantiate(
            selectedObstacle,
            spawnPoint.position,
            Quaternion.identity
        );

        // Define a velocidade do obstáculo
        ObstacleMovement movement =
            newObstacle.GetComponent<ObstacleMovement>();

        if (movement != null)
        {
            movement.SetSpeed(obstacleSpeed);
        }
    }

    private void ResetSpawnTimer()
    {
        spawnTimer = Random.Range(
            minSpawnInterval,
            maxSpawnInterval
        );
    }
}
