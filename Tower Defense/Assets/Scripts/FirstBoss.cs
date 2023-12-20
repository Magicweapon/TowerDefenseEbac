using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirstBoss : BaseEnemy
{
    public override void OnDestroy()
    {
        base.OnDestroy();
        gameManager.bossesDefeated++;
    }
}