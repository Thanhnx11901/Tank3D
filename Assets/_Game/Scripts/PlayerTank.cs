using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    
    public Slider healthSlider;

    protected override void Start()
    {
        base.Start();
        _canFire =true;
        _countTime = 0f;
        _timeFire = 1f;
        _countFire = 0;
        healthSlider.maxValue = Hp;
        healthSlider.value = Hp;
    }

    protected void Update()
    {
        if(GameManager.IsState(GameState.GamePlay) == false) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(Constants.TAG_GROUND))
        {
            Vector3 lookAtPosition = new Vector3(hit.point.x, turretTranform.position.y, hit.point.z);
            turretTranform.LookAt(lookAtPosition);
        }

        _countTime += Time.deltaTime;

        if (_countTime >= _timeFire)
        {
            _countTime = 0f;
            _countFire = 0;
        }

        if (Input.GetMouseButton(0) && _countFire < 10)
        {
            if(_canFire){
                StartCoroutine(Fire(turretTranform.forward));
            }
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
