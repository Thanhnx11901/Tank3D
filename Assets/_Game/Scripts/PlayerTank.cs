using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerTank : BaseTank
{
    public float attackSpeed = 0.15f;
    public Transform pointFire;
    public BulletTank bulletPrefab;
    public Transform turretTranform;
    private float _countTime;
    private float _timeFire;
    private int _countFire;
    private bool _canFire;
    private bool _canFireLaser;
    
    public LayerMask groundLayer;
    
    
    
    public Slider healthSlider;
    
    public float laserSpeed = 50f;
    public float laserDistance = 100f;


    protected override void Start()
    {
        base.Start();
        _canFire =true;
        _canFireLaser = true;
        _countTime = 0f;
        _timeFire = 1f;
        _countFire = 0;
        healthSlider.maxValue = Hp;
        healthSlider.value = Hp;
    }

    protected void Update()
    {
        if(GameManager.IsState(GameState.GamePlay) == false) return;
        TurretRotaion();
        
        _countTime += Time.deltaTime;
        if (_countTime >= _timeFire)
        {
            _countTime = 0f;
            _countFire = 0;
        }
        if (Input.GetMouseButton(0))
        {
            if(_canFire && _countFire < 10){
                StartCoroutine(Fire(turretTranform.forward));
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (_canFireLaser)
            {
                StartCoroutine(FireLaser());
            }
        }
    }
    private void TurretRotaion()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 lookAtPosition = new Vector3(hit.point.x, turretTranform.position.y, hit.point.z);
            turretTranform.LookAt(lookAtPosition);
        }
    }
    private IEnumerator Fire(Vector3 direction)
    {
        _canFire = false;
        
        Quaternion bulletRotation = Quaternion.LookRotation(direction);
        BulletTank bullet = Instantiate(bulletPrefab, pointFire.position, bulletRotation);
        bullet.OnInit(TypeBullet.player, direction, bulletSpeed);
        
        _countFire++;
        
        yield return new WaitForSeconds(attackSpeed);
        
        _canFire = true;
    }
    private IEnumerator FireLaser()
    {
        _canFireLaser = false;
        laserLine.enabled = true;
        laserLine.SetPosition(0, pointFire.position);
        
        Vector3 laserEndPosition = pointFire.position;
        RaycastHit hit;
        if (Physics.Raycast(pointFire.position, turretTranform.forward, out hit, 100f))
        {
            laserEndPosition = hit.point;
            
            CheckCollision(hit);
            
            ParticleSystem hitLaser = Instantiate(hitLaserPrefab, laserEndPosition, Quaternion.identity);
            hitLaser.Play();
        }
        laserLine.SetPosition(1, laserEndPosition);
        
        yield return new WaitForSeconds(.1f);
        laserLine.enabled = false;
        
        yield return new WaitForSeconds(.1f);
        _canFireLaser = true;
    }

    private void CheckCollision(RaycastHit hit)
    {
        EnemyTank enemyTank = Cache<EnemyTank>.GetCollider(hit.collider);
        if (enemyTank != null )
        {
            enemyTank.TakeDamage(Random.Range(1f, 5f));
        }
        Barrel barrel = Cache<Barrel>.GetCollider(hit.collider);
        if (barrel != null )
        {
            barrel.ExplodeBarrel();
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthSlider.value = _currentHp;
    }
    private void OnDisable()
    {
        GameManager.ChangeState(GameState.Lose);
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<Lose>();
        
    }
}
