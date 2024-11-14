using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBullet
{
    player,
    enemy,
}
public class BulletTank : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    public TypeBullet type;
    [SerializeField] protected ParticleSystem explosionPrefab;

    public void OnInit(TypeBullet typeBullet, Vector3 direction, float bulletSpeed)
    {
        type = typeBullet;
        _rb.velocity = direction * bulletSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (type == TypeBullet.enemy)
        {
            PlayerTank playerTank = Cache<PlayerTank>.GetCollider(other);
            if (playerTank != null )
            {
                playerTank.TakeDamage(Random.Range(1f, 5f));
                DestroyBullet();
            }
        }
        if (type == TypeBullet.player)
        {
            EnemyTank enemyTank = Cache<EnemyTank>.GetCollider(other);
            if (enemyTank != null )
            {
                enemyTank.TakeDamage(Random.Range(1f, 5f));
                DestroyBullet();
            }
        }
        if (other.CompareTag("Ground"))
        {
            DestroyBullet();
        }
    }
    private void DestroyBullet()
    {
        ParticleSystem particleSystem = Instantiate(explosionPrefab);
        particleSystem.transform.position = transform.position;
        particleSystem.Play();
        Destroy(gameObject);
    }
}
