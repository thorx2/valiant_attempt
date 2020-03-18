using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public string TargetTag;
    public bool AllTargetsDestroyed = false;

    [HideInInspector]
    public GameObject NearestTarget;

    [HideInInspector]
    public bool StopTargetAcquisition = false;

    private List<GameObject> possibleEnemies = null;

    void Start()
    {
        RefreshTargets();
    }

    public void RefreshTargets()
    {
        possibleEnemies = GameObject.FindGameObjectsWithTag(TargetTag).ToList();
    }

    void Update()
    {
        if (StopTargetAcquisition)
        {
            return;
        }
        
        if (possibleEnemies.Count == 0)
        {
            AllTargetsDestroyed = true;

            NearestTarget = null;

            return;
        }

        AllTargetsDestroyed = false;

        //Arbitrally large value just beacasue...
        double dist = 100000000000;

        possibleEnemies.RemoveAll( enemy => enemy == null);
        
        foreach (var enemy in possibleEnemies)
        {
            if(enemy != null)
            {
                double currentEnemyDistance = Vector3.Distance(enemy.transform.position, transform.position);
                if (dist > currentEnemyDistance)
                {
                    dist = currentEnemyDistance;

                    NearestTarget = enemy;
                }
            }
        }

        if (NearestTarget != null)
        {
            transform.LookAt (NearestTarget.transform.position);

            Quaternion rot = Quaternion.Euler( 0, transform.rotation.eulerAngles.y, 0 );

            transform.rotation = rot;
        }   
    }
}
