using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemiesDefeated;
    public int bossesDefeated;
    public int resources;
    public float yOffset;

    public delegate void ResourcesModified();
    public ResourcesModified OnResourcesModified;
    public void ModifyResources(int mod)
    {
        resources += mod;
        OnResourcesModified?.Invoke();
    }

    public void ResetValues()
    {
        enemiesDefeated = 0;
        bossesDefeated = 0;
    }
}
