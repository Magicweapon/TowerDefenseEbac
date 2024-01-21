using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour, IAttacker
{
    public Vector3 destination;
    public GameObject enemy;
    public int damage;
    private float speed;
    void Start()
    {
        var angle = TurretManager.cannonAngle * Mathf.Deg2Rad;

        var enemyPos = destination;
        enemyPos.y = transform.position.y;

        var xx = Vector3.Distance(transform.position, enemyPos);
        var yy = TurretManager.yOffset * -1.0f;

        speed = Mathf.Sqrt(   (  0.5f * 9.81f * (Mathf.Pow(xx, 2))  )  /  (  (xx * Mathf.Tan(angle)) - yy  )   )   /   Mathf.Cos(angle);

        Vector3 shotDirection;
        shotDirection = transform.rotation * Vector3.forward;
        GetComponent<Rigidbody>().velocity = shotDirection * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 2.46f)
        {
            Explode();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.gameObject;
            DealDamage(damage);
            Explode();
        }
    }
    public void Explode()
    {
        Destroy(gameObject);
    }
    public void DealDamage(int damage = 0)
    {
        enemy.GetComponent<BaseEnemy>().ReceiveDamage(damage);
    }
}
