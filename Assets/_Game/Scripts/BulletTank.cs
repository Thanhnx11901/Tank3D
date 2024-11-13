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
    private void OnTriggerEnter(Collider other)
    {
        if(type == TypeBullet.enemy)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<BaseTank>().TakeDamage(1f);
                Destroy(gameObject);
            }      
        }
        if (type == TypeBullet.player) 
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<BaseTank>().TakeDamage(1f);
                Destroy(gameObject);
            }
        }
    }
}
