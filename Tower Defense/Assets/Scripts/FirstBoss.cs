using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirstBoss : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }
}
