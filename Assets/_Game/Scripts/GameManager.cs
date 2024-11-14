using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum GameState
{
    MainMenu,
    GamePlay,
    Win,
    Lose,
}
public class GameManager : MonoBehaviour
{
    public float width = 10f;
    public PlayerTank player;
    public static GameManager Instance { get; private set; }
    public EnemyTank enemyPrefab;

    public DataColorSO dataColor;

    private float timeSpawn;
    private float timeCount;
    
    private static GameState gameState = GameState.MainMenu;

    public int score;
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
        UIManager.Ins.OpenUI<MianMenu>();
        score = 0; 
        timeCount = 5f;
        timeSpawn = 5f;
    }
    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state)
    {
        return gameState == state;
    }
    private void Update()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if(GameManager.IsState(GameState.GamePlay) == false) return;
        timeCount += Time.deltaTime;
        if (timeCount >= timeSpawn)
        {
            EnemyTank tank = Instantiate(enemyPrefab);
            tank.transform.position = GetRandomPointOnEdge();
            timeCount = 0f;
            timeSpawn -= 0.1f;
            if(timeSpawn <= 1f){
                timeSpawn = 1f;
            }
        }
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
