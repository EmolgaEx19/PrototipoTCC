using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] private Transform ground1;
    [SerializeField] private Transform ground2;

    public float groundSpeed = 5f;

    private float groundWidth;

    private void Start()
    {
        // Obtém a largura do primeiro bloco
        groundWidth = ground1.localScale.x;
    }

    private void Update()
    {
        MoveGround();
        RepositionGround();
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
}
