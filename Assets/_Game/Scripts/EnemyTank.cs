using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : BaseTank
{
    private IState<EnemyTank> currentState;
    public Transform turret;
    public Transform pointFire;
    public Transform pointRotationStart;
    public BulletTank bulletPrefab;
    public float rotationSpeedTurret;
    public float bulletSpeed = 20f;
    public float minDistance = 5f;
    public float maxDistance = 7f;
    public bool canFire = true;

    public NavMeshAgent agent;


    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshRenderer meshRenderer1;
    [SerializeField] private MeshRenderer meshRenderer2;
    [SerializeField] private MeshRenderer meshRenderer3;

    protected override void Start()
    {
        base.Start();
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
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public void TurretRotationPlayer()
    {
        Vector3 turretDirection = (GameManager.Instance.player.transform.position - transform.position).normalized;

        Quaternion desiredRotation = Quaternion.LookRotation(turretDirection, Vector3.up);

        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, desiredRotation, rotationSpeedTurret * Time.deltaTime);

        if (Quaternion.Angle(turret.transform.rotation, desiredRotation) < 1f)
        {
            if (canFire)
            {
                Fire(turret.transform.forward);
            }
        }
    }
    protected void Fire(Vector3 direction)
    {
        Quaternion bulletRotation = Quaternion.LookRotation(direction);
        BulletTank bullet = Instantiate(bulletPrefab, pointFire.position, bulletRotation);
        bullet.type = TypeBullet.enemy;
        bullet.rb.velocity = direction * bulletSpeed;
        canFire = false;
        ChangeState(new TurretState());
    }

    public void TurretRotation()
    {
        Quaternion desiredRotation = Quaternion.Euler(0, 0, 0);

        turret.transform.localRotation = Quaternion.RotateTowards(turret.transform.localRotation, desiredRotation, rotationSpeedTurret * Time.deltaTime);

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
            Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
            randomDirection += GameManager.Instance.player.transform.position;
            if (NavMesh.SamplePosition(randomDirection, out hit, maxDistance, NavMesh.AllAreas))
            {
                if (Vector3.Distance(hit.position, GameManager.Instance.player.transform.position) > minDistance)
                {
                    randomPosition = hit.position;
                    break;
                }
            }
        } while (true);

        return randomPosition;
    }


}
