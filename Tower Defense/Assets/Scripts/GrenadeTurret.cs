using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTurret : BaseTurret
{
    void Start()
    {

    }
    public override void Shoot()
    {
        foreach (GameObject tip in cannonTips)
        {
            var tempBullet = Instantiate(bulletPrefab, tip.transform.position, tip.transform.rotation);
            tempBullet.GetComponent<ExplosiveBullet>().destination = enemyPosition;
            //Debug.Log(enemy.transform.position.y); // Not correct
        }
    }
}
