using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseTank : MonoBehaviour
{
    [SerializeField] private float _hp;
    public float bulletSpeed = 20f;
    [SerializeField] protected ParticleSystem hitLaserPrefab;
    [SerializeField] protected LineRenderer laserLine;
    public float Hp
    {
        get => _hp;
        set => _hp = value;
    }             
    protected float _currentHp;  
    [SerializeField] protected ParticleSystem tankExplosionPrefab;
        
    protected virtual void Start()
    {
        _currentHp = Hp;
    }

    public virtual void TakeDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            StartCoroutine(DieCo());
        }
    }
    private IEnumerator DieCo(){
        ParticleSystem particleSystem = Instantiate(tankExplosionPrefab);
        particleSystem.transform.position = transform.position;
        particleSystem.Play();
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
}
