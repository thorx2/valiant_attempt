using System.Collections;
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

    
    void Update()
    {
        if (StopTargetAcquisition)
        {
            return;
        }

        var possibleEnemies = GameObject.FindGameObjectsWithTag(TargetTag);
        
        if (possibleEnemies.Length == 0)
        {
            AllTargetsDestroyed = true;

            NearestTarget = null;

            return;
        }

        AllTargetsDestroyed = false;

        //Arbitrally large value just beacasue...
        double dist = 100000000000;

        foreach (var enemy in possibleEnemies)
        {
            double currentEnemyDistance = Vector3.Distance(enemy.transform.position, transform.position);
            if (dist > currentEnemyDistance)
            {
                dist = currentEnemyDistance;

                NearestTarget = enemy;
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
