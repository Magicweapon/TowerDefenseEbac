using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IAttacker
{
    public Vector3 destination;
    public float speed;
    public GameObject enemy;
    public int damage;
    private void Start()
    {
        destination.y += 1f;
    }
    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        if (Vector3.Distance(transform.position, destination) < 0.05f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemy = collision.gameObject;
            DealDamage(damage);
            Destroy(gameObject);
        }
    }
    public void DealDamage(int damage)
    {
        enemy.GetComponent<BaseEnemy>().ReceiveDamage(damage);
    }
}
