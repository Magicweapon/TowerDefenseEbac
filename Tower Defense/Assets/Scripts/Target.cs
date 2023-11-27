using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health = 200;

    public delegate void DestroyedObject();
    public event DestroyedObject OnDestroyedObject;
    private void Update()
    {
        if (health <= 0)
        {
            OnDestroyedObject?.Invoke();
            Destroy(this.gameObject);
        }
    }
    public void ReceiveDamage(int damage = 20)
    {
        health -= damage;
    }
}
