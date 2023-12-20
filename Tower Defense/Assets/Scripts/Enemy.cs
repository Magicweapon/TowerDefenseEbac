using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseEnemy
{
    public override void OnDestroy()
    {
        base.OnDestroy();
        gameManager.enemiesDefeated++;
    }
}