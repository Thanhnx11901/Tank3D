using System.Collections;
using UnityEngine;

public class PlayerTank : BaseTank
{
    public Transform pointFire;
    public BulletTank bulletPrefab;
    public float bulletSpeed = 20f;
    public Transform turretTranform;
    private float countTime;
    private float timeFire;
    private int countFire;
    private bool canFire;

    protected override void Start()
    {
        base.Start();
        canFire =true;
        countTime = 0f;
        timeFire = 1f;
        countFire = 0;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Ground"))
        {
            Vector3 lookAtPosition = new Vector3(hit.point.x, turretTranform.position.y, hit.point.z);
            turretTranform.LookAt(lookAtPosition);
        }

        countTime += Time.deltaTime;

        if (countTime >= timeFire)
        {
            countTime = 0f;
            countFire = 0;
        }

        if (Input.GetMouseButton(0) && countFire < 10)
        {
            if(canFire){
                StartCoroutine(Fire(turretTranform.forward));
            }
        }
    }

    protected IEnumerator Fire(Vector3 direction)
    {
        canFire = false;
        Quaternion bulletRotation = Quaternion.LookRotation(direction);
        BulletTank bullet = Instantiate(bulletPrefab, pointFire.position, bulletRotation);
        bullet.type = TypeBullet.player;
        bullet.rb.velocity = direction * bulletSpeed;
        countFire++;
        yield return new WaitForSeconds(0.15f);
        canFire = true;
    }
}
