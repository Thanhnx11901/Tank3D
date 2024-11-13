using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public float width = 10f;
    public PlayerTank player;
    public static GameManager Instance { get; private set; }
    public EnemyTank enemyPrefab;

    private float timeSpawn;
    private float timeCount;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        timeCount = 5f;
        timeSpawn = 5f;
    }
    private void Update()
    {
        if (timeCount >= timeSpawn)
        {
            SpawnEnemy();
            timeCount = 0f;
            timeSpawn -= 0.1f;
        }
        timeCount += Time.deltaTime;
    }
    public void SpawnEnemy()
    {
        EnemyTank tank = Instantiate(enemyPrefab);
        tank.transform.position = GetRandomPointOnEdge();
    }

    public Vector3 GetRandomPointOnEdge()
    {
        int edgeIndex = Random.Range(0, 4);
        float random = Random.Range(-width / 2, width / 2);
        switch (edgeIndex)
        {
            case 0: 
                return new Vector3(random, 0.3f, width);
            case 1:
                return new Vector3(random, 0.3f, -width);
            case 2:
                return new Vector3(-width, 0.3f, random);
            case 3:
                return new Vector3(width, 0.3f, random);

            default:
                return Vector3.zero;
        }
    }
    
}
