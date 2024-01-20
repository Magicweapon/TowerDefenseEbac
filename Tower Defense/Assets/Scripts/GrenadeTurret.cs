using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTurret : BaseTurret, IAttacker
{
    void Start()
    {

    }
    public override void Shoot()
    {
        foreach (GameObject tip in cannonTips)
        {
            var tempBullet = Instantiate(bulletPrefab, tip.transform.position, tip.transform.rotation);
            tempBullet.GetComponent<ExplosiveBullet>().destination = enemy.transform.position;
        }
    }
    public void DealDamage(int damage = 0)
    {
        enemy.GetComponent<BaseEnemy>().ReceiveDamage(damage);
    }
}
