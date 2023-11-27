using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirstBoss : MonoBehaviour
{
    public GameObject target;
    public int health;

    public Animator Anim;

    private void OnEnable()
    {
        target = GameObject.Find("Target");
    }

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

    public void DealDamage(int damage)
    {
        target?.GetComponent<Target>().ReceiveDamage(damage);
    }

    public void ReceiveDamage(int damage = 5)
    {
        health -= damage;
    }
}
