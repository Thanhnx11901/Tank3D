using System.Collections;
using UnityEngine;

public class BaseTank : MonoBehaviour
{
    [SerializeField] protected float hp;             
    protected float currentHp;  
    [SerializeField] protected ParticleSystem tankExplosionPrefab;
        
    protected virtual void Start()
    {
        currentHp = hp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
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
