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
        var angle = TurretManager.cannonAngle;
        var yy = TurretManager.yOffset * -1.0f;
        var enemyPos = destination;
        //enemyPos.y -= yy;
        enemyPos.y = transform.position.y;
        var xx = Vector3.Distance(transform.position, enemyPos);

        Debug.Log($"angle: {angle}");
        Debug.Log($"Y: {yy}");
        Debug.Log($"Enemy Position{enemyPos}");
        Debug.Log($"X: {xx}");

        speed = Mathf.Sqrt(   (0.5f * 9.81f * (Mathf.Pow(xx, 2))) / ((xx * (Mathf.Sin(angle) / Mathf.Cos(angle))) - yy)   ) / Mathf.Cos(angle);
        Debug.Log($"Speed: {speed}");
        Vector3 shotDirection;
        shotDirection = transform.rotation * Vector3.forward;
        GetComponent<Rigidbody>().velocity = shotDirection * 24.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 0.05f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.gameObject;
            DealDamage(damage);
            //Destroy(gameObject);
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
