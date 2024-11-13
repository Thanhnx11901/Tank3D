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
    public Rigidbody rb;
    public TypeBullet type;
    [SerializeField] protected ParticleSystem explosionPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (type == TypeBullet.enemy)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<BaseTank>().TakeDamage(Random.Range(1f, 5f));
                DestroyBullet();
            }
        }
        if (type == TypeBullet.player)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<BaseTank>().TakeDamage(Random.Range(1f, 5f));
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
