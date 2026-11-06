using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerController pc;

    [SerializeField] private float scoreMultiplier = 1f;

    private float distance;

    /*private void Awake()
    {
        gm = GetComponent<GroundMovement>();
        pc = GetComponent<PlayerController>();
    }*/

    private void Update()
    {
        if (!pc.isAlive)
            return;

        CalculateScore();
    }

    private void CalculateScore()
    {
        distance += GroundMovement.Instance.GetSpeed() * Time.deltaTime;

        //Debug.Log("Score: " + GetScore());
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(distance * scoreMultiplier);
    }
}
