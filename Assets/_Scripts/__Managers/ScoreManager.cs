using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerController pc;

    private float distance;

    private void Update()
    {
        if (!pc.isAlive)
            return;

        CalculateScore();
    }

    private void CalculateScore()
    {
        distance += GroundMovement.Instance.GetSpeed() * Time.deltaTime;
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(distance);
    }
}
