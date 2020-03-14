using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 cameraPosOffset;

    public float Speed;
    public Joystick joycon;
    private GameObject nearestEnemy;

    public Transform RespawnPoint;

    bool enemyFound = false;

    bool gameOver = false;

    private WeponMechanic weponMechanic;

    void Start()
    {
        cameraPosOffset = Camera.main.transform.position - transform.position;

        transform.position = RespawnPoint.position;

        weponMechanic = GetComponent<WeponMechanic>();

        weponMechanic.StartFiring();
    }

    // Update is called once per frame
    void Update()
    {
        float stride = Speed * Time.deltaTime;
        
        if (joycon.Horizontal != 0 || joycon.Vertical != 0)
        {
            Vector3 joyconDirection = new Vector3 (transform.position.x + joycon.Horizontal, 0.5f, transform.position.z + joycon.Vertical);

            transform.LookAt (joyconDirection);

            Quaternion rot = Quaternion.Euler( 0, transform.rotation.eulerAngles.y, 0 );

            transform.rotation = rot;

            enemyFound = false;

            transform.position = Vector3.MoveTowards (transform.position, joyconDirection, stride);

            weponMechanic.StopFiring();
        }
        else
        {
            if (!enemyFound && !gameOver)
            {
                FindNearestEnemy();
            }
        }
    }

    void FindNearestEnemy()
    {
        var possibleEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (possibleEnemies.Length == 0)
        {
            gameOver = true;
            weponMechanic.StopFiring();
            return;
        }

        //Arbitrally large value just beacasue...
        double dist = 100000000000;

        foreach (var enemy in possibleEnemies)
        {
            double currentEnemyDistance = Vector3.Distance(enemy.transform.position, transform.position);
            if (dist > currentEnemyDistance)
            {
                dist = currentEnemyDistance;

                nearestEnemy = enemy;

                enemyFound = true;
            }
        }

        if (nearestEnemy != null)
        {
            transform.LookAt (nearestEnemy.transform.position);

            Quaternion rot = Quaternion.Euler( 0, transform.rotation.eulerAngles.y, 0 );

            transform.rotation = rot;

            weponMechanic.StartFiring();
        }
    }

    void LateUpdate()
    {
        Camera.main.transform.position = transform.position + cameraPosOffset;
    }
}
