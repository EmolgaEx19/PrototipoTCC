using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public static GroundMovement Instance;

    [SerializeField] private Transform ground1;
    [SerializeField] private Transform ground2;

    [Header("Movement Settings")]
    [SerializeField] private float groundSpeed = 5f;
    [SerializeField] private float speedIncreaseRate = 0.05f;
    [SerializeField] private float maxSpeed = 40f;

    private float currentIncreaseRate;

    private float groundWidth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Obtém a largura do primeiro bloco
        groundWidth = ground1.localScale.x;

        ApplySpeedBonus();
    }

    private void Update()
    {
        IncreaseSpeed();
        MoveGround();
        RepositionGround();

        Debug.Log(
        $"Velocidade atual: {groundSpeed:F3} | " +
        $"Aceleração: {currentIncreaseRate:F3}"
    );
    }

    private void ApplySpeedBonus()
    {
        currentIncreaseRate = speedIncreaseRate;

        if (GameManager.Instance == null)
            return;

        OutfitStats stats =
            GameManager.Instance.GetTotalOutfitStats();

        if (stats.speed <= 0)
            return;

        // Cada ponto reduz a aceleração em 1%
        // O limite máximo de redução é 50%
        float reduction = Mathf.Clamp(
            stats.speed * 0.01f,
            0f,
            0.5f
        );

        currentIncreaseRate =
            speedIncreaseRate * (1f - reduction);

        Debug.Log(
            "Aceleração com bônus: " +
            currentIncreaseRate
        );
    }


    private void IncreaseSpeed()
    {
        if(groundSpeed < maxSpeed)
        {
            groundSpeed += currentIncreaseRate * Time.deltaTime;
            groundSpeed = Mathf.Min(groundSpeed, maxSpeed);
        }
    }

    private void MoveGround()
    {
        // Move os dois blocos para a esquerda
        Vector3 movement = Vector3.left * groundSpeed * Time.deltaTime;

        ground1.position += movement;
        ground2.position += movement;
    }

    private void RepositionGround()
    {
        // Verifica se o primeiro bloco saiu completamente
        if (ground1.position.x <= -groundWidth)
        {
            ground1.position = new Vector3(
                ground2.position.x + groundWidth,
                ground1.position.y,
                ground1.position.z
            );
        }

        // Verifica se o segundo bloco saiu completamente
        if (ground2.position.x <= -groundWidth)
        {
            ground2.position = new Vector3(
                ground1.position.x + groundWidth,
                ground2.position.y,
                ground2.position.z
            );
        }
    }

    public float GetSpeed()
    {
        return groundSpeed;
    }
}
