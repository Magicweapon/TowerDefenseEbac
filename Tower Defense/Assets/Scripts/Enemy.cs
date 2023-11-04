using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public int health;

    public Animator Anim;

    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        Anim = GetComponent<Animator>();
        Anim.SetBool("isMoving", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Anim.SetBool("isMoving", false);
            Anim.SetTrigger("OnTargetMet");
        }
    }

    public void DealDamage()
    {
        target?.GetComponent<Target>().ReceiveDamage(40);
    }

    public void ReceiveDamage(int damage = 5)
    {
        health -= damage;
    }
}
