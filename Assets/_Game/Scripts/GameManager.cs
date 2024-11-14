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
    Lose,
}
public class GameManager : MonoBehaviour
{
    public float width = 10f;
    public PlayerTank player;
    public static GameManager Instance { get; private set; }
    public EnemyTank enemyPrefab;

    public DataColorSO dataColor;

    private float _timeSpawnEnemy;
    private float _timeCountEnemy;
    
    private float _timeSpawnBarrel;
    private float _timeCountBarrel;
    
    private static GameState gameState = GameState.MainMenu;
    
    [SerializeField] private Barrel barrelPrefab;

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
        _timeCountEnemy = 5f;
        _timeSpawnEnemy = 5f;
        _timeCountBarrel = 7f;
        _timeSpawnBarrel = 7f;
        
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
        SpawnBarrel();
    }
    private void SpawnBarrel()
    {
        _timeCountBarrel += Time.deltaTime;
        if (_timeCountBarrel >= _timeSpawnBarrel)
        {
            Barrel barrel =  Instantiate(barrelPrefab);
            barrel.transform.position = GetRandomPosition();
            _timeCountBarrel = 0f;
        }
    }
    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-8f, 8f), 0, Random.Range(-8f, 8f));
    }
    private void SpawnEnemy()
    {
        if(GameManager.IsState(GameState.GamePlay) == false) return;
        _timeCountEnemy += Time.deltaTime;
        if (_timeCountEnemy >= _timeSpawnEnemy)
        {
            EnemyTank tank = Instantiate(enemyPrefab);
            tank.transform.position = GetRandomPointOnEdge();
            _timeCountEnemy = 0f;
            _timeSpawnEnemy -= 0.1f;
            if(_timeSpawnEnemy <= 1f){
                _timeSpawnEnemy = 1f;
            }
        }
    }
    private Vector3 GetRandomPointOnEdge()
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
