using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, IAttacker, IAttackable
{
    public GameObject target;
    public int health;
    public int defaultDamage;
    public int earnedResources;

    public GameManager gameManager;
    public EnemySpawner enemySpawner;
    public Animator Anim;
    private void OnEnable()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        target = GameObject.Find("Target");
        target.GetComponent<Target>().OnDestroyedObject += Stop;
    }
    private void OnDisable()
    {
        if (target) target.GetComponent<Target>().OnDestroyedObject -= Stop;
    }
    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        Anim = GetComponent<Animator>();
        Anim.SetBool("isMoving", true);
    }
    void Update()
    {
        if (health <= 0)
        {
            Anim.SetTrigger("OnDeath");
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
            Destroy(gameObject, 3);
        }
    }
    public virtual void OnDestroy()
    {
        gameManager.ModifyResources(earnedResources);
        enemySpawner.EnemiesOnScene.Remove(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Anim.SetBool("isMoving", false);
            Anim.SetTrigger("OnTargetMet");
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
    }
    public void DealDamage(int damage)
    {
        if (damage == 0) damage = defaultDamage;
        target?.GetComponent<Target>().ReceiveDamage(damage);
    }
    public void ReceiveDamage(int damage = 5)
    {
        health -= damage;
    }
    public void Stop()
    {
        Anim.SetTrigger("OnTargetDestroyed");
        Anim.SetBool("isMoving", false);
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
    }
}
