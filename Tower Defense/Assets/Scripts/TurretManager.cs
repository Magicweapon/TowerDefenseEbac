using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretManager : MonoBehaviour
{
    public TouchManager touchManager;
    public GameManager gameManager;
    public EnemySpawner enemySpawner;
    public GameObject target;

    public List<GameObject> turretPrefabs;
    public List<GameObject> instantiatedTurrets;

    public delegate void EnemyReset();
    public event EnemyReset OnEnemyReset;
    public enum SelectedTurret
    {
        Tower1,
        Tower2,
        Tower3,
        Tower4,
        Tower5
    }

    public SelectedTurret selectedTurret;

    private void OnEnable()
    {
        touchManager.OnPlatformClicked += CreateTurret;
        enemySpawner.OnWaveStarted += ResetEnemy;
        instantiatedTurrets = new List<GameObject>();
    }
    private void OnDisable()
    {
        touchManager.OnPlatformClicked -= CreateTurret;
        enemySpawner.OnWaveStarted -= ResetEnemy;
    }
    private void ResetEnemy()
    {
        if (enemySpawner.WaveHasStarted)
        {
            float shortestDistance = float.MaxValue;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemySpawner.EnemiesOnScene)
            {
                float dist = Vector3.Distance(enemy.transform.position, target.transform.position);
                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null)
            {
                foreach (GameObject turret in instantiatedTurrets)
                {
                    turret.GetComponent<BaseTurret>().enemy = nearestEnemy;
                    turret.GetComponent<BaseTurret>().Shoot();
                }

                OnEnemyReset?.Invoke();
            }
            Invoke("ResetEnemy", 3f);
        }
    }
    private void CreateTurret(GameObject platform)
    {
        int cost = selectedTurret switch
        {
            SelectedTurret.Tower1 => 400,
            SelectedTurret.Tower2 => 600,
            SelectedTurret.Tower3 => 900,
            SelectedTurret.Tower4 => 750,
            SelectedTurret.Tower5 => 1100,
            _ => 0
        };

        if (platform.transform.childCount == 0 && gameManager.resources >= cost)
        {
            gameManager.ModifyResources(-cost);
            int turretIndex = (int)selectedTurret;
            Vector3 turretPosition = platform.transform.position;
            turretPosition.y += 1;
            GameObject Turret = Instantiate(turretPrefabs[turretIndex], turretPosition, Quaternion.identity);
            Turret.transform.SetParent(platform.transform);
            instantiatedTurrets.Add(Turret);
        }
    }

    public void SetTurret(int tower)
    {
        if (Enum.IsDefined(typeof(SelectedTurret), tower))
        {
            selectedTurret = (SelectedTurret)tower;
        }
        else
        {
            Debug.Log("This tower is not defined.");
        }
    }
}
