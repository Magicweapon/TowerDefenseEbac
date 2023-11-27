using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretManager : MonoBehaviour
{
    public TouchManager touchManager;

    public List<GameObject> turretPrefabs;

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
    }
    private void OnDisable()
    {
        touchManager.OnPlatformClicked -= CreateTurret;
    }

    private void CreateTurret(GameObject platform)
    {
        if (platform.transform.childCount > 0) return;
        Debug.Log("Creating turret...");
        int turretIndex = (int)selectedTurret;
        Vector3 turretPosition = platform.transform.position;
        turretPosition.y += 1;
        GameObject Turret = Instantiate(turretPrefabs[turretIndex], turretPosition, Quaternion.identity);
        Turret.transform.SetParent(platform.transform);
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
