using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health = 200;

    public void ReceiveDamage(int damage = 20)
    {
        health -= damage;
    }
}
