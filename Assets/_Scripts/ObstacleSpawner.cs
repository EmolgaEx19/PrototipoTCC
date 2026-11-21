
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public enum SpawnType
    {
        Obstacle,
        Platform,
        Coin,
        ItemBox
    }

    [System.Serializable]
    public class SpawnElement
    {
        public GameObject prefab;
        public SpawnType type;

        [Min(0f)]
        public float weight = 1f;
    }

    [SerializeField] private SpawnElement[] spawnElements;

    [Header("Configurações do Spawn")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 4f;

    [Header("Bônus de Sorte")]
    [SerializeField] private float luckBonusPerPoint = 0.01f;
    [SerializeField] private float maxLuckBonus = 0.5f;
    private float luckMultiplier = 1f;

    private float spawnTimer;

    private void Start()
    {
        ApplyLuckBonus();
    }


    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            ResetSpawnTimer();
            SpawnObstacle();  
        }
    }

    private void ApplyLuckBonus()
    {
        luckMultiplier = 1f;

        if (GameManager.Instance == null)
            return;

        OutfitStats stats =
            GameManager.Instance.GetTotalOutfitStats();

        float luckBonus = Mathf.Clamp(
            stats.luck * luckBonusPerPoint,
            0f,
            maxLuckBonus
        );

        luckMultiplier = 1f + luckBonus;
    }

    private void SpawnObstacle()
    {
        if (spawnElements == null ||
            spawnElements.Length == 0 ||
            spawnPoint == null)
        {
            return;
        }

        GameObject selectedPrefab = ChoosePrefabByWeight();

        if (selectedPrefab == null)
            return;

        GameObject newObstacle = Instantiate(
            selectedPrefab,
            spawnPoint.position,
            Quaternion.identity
        );
    }

    private GameObject ChoosePrefabByWeight()
    {
        float totalWeight = 0f;

        foreach (SpawnElement element in spawnElements)
        {
            totalWeight += GetEffectiveWeight(element);
        }

        if (totalWeight <= 0f)
            return null;

        float randomValue = Random.Range(0f, totalWeight);

        float currentWeight = 0f;
        GameObject lastValidPrefab = null;

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

    private float GetEffectiveWeight(SpawnElement element)
    {
        if (element == null || element.prefab == null)
            return 0f;

        float baseWeight = Mathf.Max(0f, element.weight);

        if (element.type == SpawnType.Coin ||
            element.type == SpawnType.ItemBox)
        {
            return baseWeight * luckMultiplier;
        }

        return baseWeight;
    }

    private void ResetSpawnTimer()
    {
        spawnTimer = Random.Range(
            minSpawnInterval,
            maxSpawnInterval
        );
    }
}
