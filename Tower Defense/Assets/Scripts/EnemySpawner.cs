using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesPrefabs;
    public int wave;
    public List<int> enemiesPerWave;

    private int enemiesForThisWave;

    public bool WaveHasStarted;
    public List<GameObject> EnemiesOnScene;

    public delegate void WaveState();
    public event WaveState OnWaveStarted;
    public event WaveState OnWaveFinished;
    public event WaveState OnWaveDefeated;
    void Start()
    {
        wave = 0;
    }
    private void FixedUpdate()
    {
        if (WaveHasStarted && EnemiesOnScene.Count == 0)
        {
            WinWave();
        }
    }
    public void StartWave()
    {
        if (wave == enemiesPerWave.Count)
        {
            return;
        }
        WaveHasStarted = true;
        OnWaveStarted?.Invoke();
        SetEnemiesQuantity();
        InstantiateEnemy();
    }
    public void WinWave()
    {
        if (WaveHasStarted)
        {
            OnWaveDefeated?.Invoke();
            WaveHasStarted = false;
        }
    }
    public void FinishWave()
    {
        OnWaveFinished?.Invoke();
    }
    public void SetEnemiesQuantity()
    {
        enemiesForThisWave = enemiesPerWave[wave];
    }
    public void InstantiateEnemy()
    {
        int randomIndex = Random.Range(0, enemiesPrefabs.Count);
        var tempEnemy = Instantiate<GameObject>(enemiesPrefabs[randomIndex], transform.position, Quaternion.identity);
        var angle = tempEnemy.transform.eulerAngles;
        angle.y += 180;
        tempEnemy.transform.eulerAngles = angle;
        EnemiesOnScene.Add(tempEnemy);
        enemiesForThisWave--;

        if (enemiesForThisWave == 0)
        {
            wave++;
            //SetEnemiesQuantity();
            FinishWave();
            return;
        }

        Invoke("InstantiateEnemy", 1.0f + 1.0f / 3.0f);
    }
}
