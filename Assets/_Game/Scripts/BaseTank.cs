using UnityEngine;

public class BaseTank : MonoBehaviour
{
    protected float attackSpeed;
    [SerializeField] protected float hp;             
    [SerializeField] protected float currentHp;       

    protected virtual void Start()
    {
        currentHp = hp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
