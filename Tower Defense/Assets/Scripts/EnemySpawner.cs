using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesPrefabs;
    public int wave;
    public List<int> enemiesPerWave;

    private int enemiesForThisWave;

    public delegate void WaveFinished();
    public event WaveFinished OnWaveFinished;
    void Start()
    {
        wave = 0;
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
        Instantiate<GameObject>(enemiesPrefabs[randomIndex], transform.position, Quaternion.identity);
        enemiesForThisWave--;
        Debug.Log(enemiesForThisWave);

        if (enemiesForThisWave == 0)
        {
            wave++;
            //SetEnemiesQuantity();
            FinishWave();
            return;
        }

        Invoke("InstantiateEnemy", 2f);
    }
}
