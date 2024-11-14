using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionPrefab;
    public float detectionRange = 3f;
    [SerializeField] private float _dame;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private BoxCollider boxCollider;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BULLET))
        {
            ExplodeBarrel();
        }
    }
    public void ExplodeBarrel()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, detectionRange);
            
        foreach (Collider col in objectsInRange)
        {
            BaseTank baseTank = Cache<BaseTank>.GetCollider(col);
            if (baseTank != null)
            {
                baseTank.TakeDamage(_dame);
            }
        }
        ParticleSystem particleSystem = Instantiate(explosionPrefab);
        particleSystem.transform.position = transform.position;
        particleSystem.Play();
        Destroy(gameObject);
        //StartCoroutine(DisableBarrel());
    }
    private IEnumerator DisableBarrel()
    {
        renderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(5f);  
        renderer.enabled = true;
        boxCollider.enabled = true;
    }
}
