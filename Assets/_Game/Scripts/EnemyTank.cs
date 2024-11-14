using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyTank : BaseTank
{
    private IState<EnemyTank> _currentState;
    
    public Transform turret;
    public Transform pointFire;
    
    public BulletTank bulletPrefab;
    
    [SerializeField] private float _rotationSpeedTurret;
    
    private float _minDistance;
    private float _maxDistance;
    
    private bool _canFire = true;
    public bool CanFire
    {
        get => _canFire;
        set => _canFire = value;
    }

    public NavMeshAgent agent;
    
    public Slider healthBar;
    private float _countTimeShowHealthBar;
    private float _timeShowHealthBar;
    
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshRenderer meshRenderer1;
    [SerializeField] private MeshRenderer meshRenderer2;
    [SerializeField] private MeshRenderer meshRenderer3;

    protected override void Start()
    {
        base.Start();
        OnInit();
    }

    public void OnInit()
    {
        _timeShowHealthBar = 1f;
        _countTimeShowHealthBar = 0f;
        healthBar.gameObject.SetActive(false);
        healthBar.maxValue = Hp;
        healthBar.value = Hp;

        _minDistance = 5f;
        _maxDistance = 7f;
        
        RandomColorTank();
        ChangeState(new IdleState());
    }
    public void RandomColorTank()
    {
        Color colorRandom = GameManager.Instance.dataColor.GetColorRandom();
        meshRenderer.materials[0].color = colorRandom;
        meshRenderer1.materials[0].color = colorRandom;
        meshRenderer2.materials[0].color = colorRandom;
        meshRenderer3.materials[0].color = colorRandom;
    }

    public void ChangeState(IState<EnemyTank> state)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this);
        }

        _currentState = state;

        if (_currentState != null)
        {
            _currentState.OnEnter(this);
        }
    }
    private void Update()
    {
        if(GameManager.IsState(GameState.GamePlay) == false) return;
        if (_currentState != null)
        {
            _currentState.OnExecute(this);
        }
    }
    public void TurretRotationPlayer()
    {
        Vector3 turretDirection = (GameManager.Instance.player.transform.position - transform.position).normalized;

        Quaternion desiredRotation = Quaternion.LookRotation(turretDirection, Vector3.up);

        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, desiredRotation, _rotationSpeedTurret * Time.deltaTime);

        if (Quaternion.Angle(turret.transform.rotation, desiredRotation) < 1f)
        {
            if (_canFire)
            {
                Fire(turret.transform.forward);
            }
        }
    }
    private void Fire(Vector3 direction)
    {
        Quaternion bulletRotation = Quaternion.LookRotation(direction);
        BulletTank bullet = Instantiate(bulletPrefab, pointFire.position, bulletRotation);
        bullet.OnInit(TypeBullet.enemy,direction,bulletSpeed);
        
        _canFire = false;
        
        ChangeState(new TurretRotationStartState());
    }

    public void TurretRotationStart()
    {
        Quaternion desiredRotation = Quaternion.Euler(0, 0, 0);

        turret.transform.localRotation = Quaternion.RotateTowards(turret.transform.localRotation, desiredRotation, _rotationSpeedTurret * Time.deltaTime);

        if (Quaternion.Angle(turret.transform.localRotation, desiredRotation) < 1f)
        {
            ChangeState(new IdleState());
        }
    }
    public Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;
        NavMeshHit hit;

        do
        {
            Vector3 randomDirection = Random.insideUnitSphere * _maxDistance;
            randomDirection += GameManager.Instance.player.transform.position;
            if (NavMesh.SamplePosition(randomDirection, out hit, _maxDistance, NavMesh.AllAreas))
            {
                if (Vector3.Distance(hit.position, GameManager.Instance.player.transform.position) > _minDistance)
                {
                    randomPosition = hit.position;
                    break;
                }
            }
        } while (true);

        return randomPosition;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(ShowHealthBar());
    }

    private IEnumerator ShowHealthBar()
    {
        healthBar.gameObject.SetActive(true);
        healthBar.value = _currentHp;
        _countTimeShowHealthBar = 0f;
        while (_countTimeShowHealthBar <= _timeShowHealthBar)
        {
            _countTimeShowHealthBar+= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        healthBar.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance.score += 1;
    }
}
