using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public GameObject enemy;
    public GameObject bulletPrefab;
    public List<GameObject> cannonTips;
    void Update()
    {
        if (enemy != null)
        {
            Aim();
        }
    }
    private void Aim()
    {
        transform.GetChild(2).LookAt(enemy.transform);
        //transform.LookAt(enemy.transform);
    }
    public virtual void Shoot()
    {
        foreach (GameObject tip in cannonTips)
        {
            var tempBullet = Instantiate(bulletPrefab, tip.transform.position, Quaternion.identity);
            tempBullet.GetComponent<Bullet>().destination = enemy.transform.position;
        }
    }
}
