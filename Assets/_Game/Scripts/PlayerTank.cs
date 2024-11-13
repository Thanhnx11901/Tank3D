using UnityEngine;

public class PlayerTank : BaseTank
{
    public float distanceFromCamera = 10f;
    public Transform pointFire;
    public BulletTank bulletPrefab;
    public float bulletSpeed = 20f;

    public float countTime;
    public float timeFire;

    public int countFire;

    protected override void Start()
    {
        base.Start();
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
            Vector3 lookAtPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            transform.LookAt(lookAtPosition);
        }

        countTime += Time.deltaTime;    
        if(countTime >= timeFire){
            countTime = 0f; 
            countFire = 0;
        }

        if (Input.GetMouseButtonDown(0) &&  countFire < 10)
        {
            Fire(transform.forward);
            countFire++;
        }
    }

    protected void Fire(Vector3 direction)
    {
        BulletTank bullet = Instantiate(bulletPrefab, pointFire.position, Quaternion.identity);
        bullet.type = TypeBullet.player;
        bullet.rb.velocity = direction * bulletSpeed;
    }
}
