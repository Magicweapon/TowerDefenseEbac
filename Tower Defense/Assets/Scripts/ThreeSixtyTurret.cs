using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSixtyTurret : BaseTurret
{
    public override void Update()
    {

    }
    public override void Shoot()
    {
        foreach (GameObject tip in cannonTips)
        {
            Instantiate(bulletPrefab, tip.transform.position, tip.transform.rotation);
        }
    }
}
