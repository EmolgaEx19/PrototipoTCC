
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleSpawner : MonoBehaviour
{
    public enum SpawnType
    {
        Obstacle,
        Platform,
        Coin,
        ItemBox
    }

    //Armazena cada elemento e sua probabilidade
    [System.Serializable]
    public class SpawnElement
    {
        public GameObject prefab;
        public SpawnType type;

        [Header("Pesos para Spawn")]
        [Min(0f)] public float initialWeight = 1f;
        [Min(0f)] public float finalWeight = 1f;
    }

    [SerializeField] private SpawnElement[] spawnElements;

    [Header("Configurações do Spawn")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float initialSpawnDelay = 2.5f;
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 4f;

    [Header("Progressão dos Pesos")]
    [SerializeField, Min(0.01f)]
    private float progressionDuration = 120f;

    private float elapsedTime = 0f;
    private float spawnProgress = 0f;

    [Header("Bônus de Sorte")]
    [SerializeField] private float luckBonusPerPoint = 0.01f;
    [SerializeField] private float maxLuckBonus = 0.5f;
    private float luckMultiplier = 1f;

    [SerializeField] private bool showSpawnLogs = false;

    private float spawnTimer;

    private void Start()
    {
        ApplyLuckBonus();
        spawnTimer = initialSpawnDelay;

        elapsedTime = 0f;
        spawnProgress = 0f;
    }


    private void Update()
    {
        UpdateSpawnProgress();

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            ResetSpawnTimer();
            SpawnObstacle();  
        }
    }

    // Calcula a progressão entre 0 e 1
    private void UpdateSpawnProgress()
    {
        elapsedTime += Time.deltaTime;

        spawnProgress = Mathf.Clamp01(
            elapsedTime / progressionDuration
        );
    }

    // Calcula o bônus das roupas uma vez por rodada
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

        if (showSpawnLogs)
        {
            /*Debug.Log(
                $"[{name}] Multiplicador de Sorte: " +
                luckMultiplier.ToString("0.##")
            );*/
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
        /*GameObject newObstacle = */
        Instantiate(
            selectedPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        if (showSpawnLogs)
        {
            /*Debug.Log(
                $"[{name}] Spawnou: {selectedPrefab.name}" +
                $"Progressão: {spawnProgress:P0}"
            );*/
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

        float currentWeight = Mathf.Lerp(
            Mathf.Max(0f, element.initialWeight),
            Mathf.Max(0f, element.finalWeight),
            spawnProgress
        );

        if (element.type == SpawnType.Coin ||
            element.type == SpawnType.ItemBox)
        {
            currentWeight *= luckMultiplier;    
        }

        return currentWeight;
    }

    private void ResetSpawnTimer()
    {
        float minInterval = Mathf.Max(0.01f, minSpawnInterval);

        float maxInterval = Mathf.Max(
            minInterval,
            maxSpawnInterval
        );

        spawnTimer = Random.Range(
            minSpawnInterval,
            maxSpawnInterval
        );
    }
}
