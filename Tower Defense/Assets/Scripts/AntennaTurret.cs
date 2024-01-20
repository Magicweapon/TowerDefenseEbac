using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaTurret : BaseTurret, IAttacker
{
    public float rayDivisions;
    public LineRenderer lineRenderer;
    public List<Vector3> points;
    public int rayPower;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        if (enemy != null)
        {
            Shoot();
            DealDamage(rayPower);
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }
    public override void Shoot()
    {
        points = ObtainPoints();
        points.Insert(0, cannonTips[0].transform.position);
        var enemyPos = enemy.transform.position;
        enemyPos.y += 1.0f;
        points.Add(enemyPos);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
    private List<Vector3> ObtainPoints()
    {
        List<Vector3> tempPoints = new List<Vector3>();
        float divider = 1.0f / rayDivisions;
        float linear = 0.0f;
        bool isPositive = false;
        Vector3 enemyPos = enemy.transform.position;
        enemyPos.y += 1.0f;

        if (rayDivisions == 0)
        {
            Debug.Log("Cannot divide by zero.");
            return null;
        }

        if (rayDivisions == 1)
        {
            var point = Vector3.Lerp(cannonTips[0].transform.position, enemyPos, 0.5f);
            tempPoints.Add(point);
            return tempPoints;
        }

        for (int i = 0; i < rayDivisions; i++)
        {
            if (i == 0)
            {
                linear = divider / 2;
            }
            else
            {
                linear += divider;
            }

            var point = Vector3.Lerp(cannonTips[0].transform.position, enemyPos, linear);

            point += Random.insideUnitSphere * 0.7f;

            //if (isPositive)
            //{

            //    point.x += Random.value * 2;
            //    isPositive = false;
            //}
            //else
            //{
            //    point.x -= Random.value * 2;
            //    isPositive = true;
            //}

            tempPoints.Add(point);
        }

        return tempPoints;
    }
    public void DealDamage(int damage = 0)
    {
        enemy.GetComponent<BaseEnemy>().ReceiveDamage(damage);
    }
}
