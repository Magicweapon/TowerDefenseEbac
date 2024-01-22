using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDirBullet : Bullet
{
    void Start()
    {
        Destroy(gameObject, 3.0f);
    }
    public override void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
