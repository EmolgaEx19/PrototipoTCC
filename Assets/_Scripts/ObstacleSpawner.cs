
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //Armazena cada elemento e sua probabilidade
    [System.Serializable]
    public class SpawnElement
    {
        public GameObject prefab;

        [Min(0f)]
        public float weight = 1f;
    }

    [SerializeField] private SpawnElement[] spawnElements;

    [Header("Configurações do Spawn")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 4f;

    [SerializeField] private bool showSpawnLogs = false;

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
        if (spawnElements == null ||
            spawnElements.Length == 0 ||
            spawnPoint == null)
        {
            return;
        }

        // Seleciona um Prefab com base nos pesos.
        GameObject selectedPrefab = ChoosePrefabByWeight();

        if (selectedPrefab == null)
            return;

        // Instancia o elemento selecionado.
        GameObject newObstacle = Instantiate(
            selectedPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        if (showSpawnLogs)
        {
            Debug.Log(
                $"[{name}] Spawnou: {selectedPrefab.name}"
            );
        }
    }

    private GameObject ChoosePrefabByWeight()
    {
        float totalWeight = 0f;

        // Calcula a soma dos pesos disponíveis.
        foreach (SpawnElement element in spawnElements)
        {
            totalWeight += GetEffectiveWeight(element);
        }

        if (totalWeight <= 0f)
            return null;

        // Sorteia um valor entre 0 e o peso total.
        float randomValue = Random.Range(0f, totalWeight);

        float currentWeight = 0f;
        GameObject lastValidPrefab = null;

        // Identifica o elemento correspondente.
        foreach (SpawnElement element in spawnElements)
        {
            float weight = GetEffectiveWeight(element);

            if (weight <= 0f)
                continue;

            currentWeight += weight;
            lastValidPrefab = element.prefab;

            if (randomValue < currentWeight)
            {
                return element.prefab;
            }
        }

        return lastValidPrefab;
    }

    // Centraliza o cálculo dos pesos.
    // Futuramente receberá os bônus das roupas.
    private float GetEffectiveWeight(SpawnElement element)
    {
        if (element == null || element.prefab == null)
            return 0f;

        return Mathf.Max(0f, element.weight);
    }

    private void ResetSpawnTimer()
    {
        spawnTimer = Random.Range(
            minSpawnInterval,
            maxSpawnInterval
        );
    }
}
