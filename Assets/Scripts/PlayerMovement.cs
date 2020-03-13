using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 cameraPosOffset;

    public float Speed;
    public Joystick joycon;
    private GameObject nearestEnemy;

    bool enemyFound = false;

    void Start()
    {
        cameraPosOffset = Camera.main.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float stride = Speed * Time.deltaTime;
        
        if (joycon.Horizontal != 0 || joycon.Vertical != 0)
        {
            transform.LookAt (new Vector3 (joycon.Horizontal, 0, joycon.Vertical));

            Quaternion rot = Quaternion.Euler( 0, transform.rotation.eulerAngles.y, 0 );

            transform.rotation = rot;

            enemyFound = false;
        }
        else
        {
            // Find next enemy and attack
        }
    }

    void FindNearestEnemy()
    {

    }

    void LateUpdate()
    {
        Camera.main.transform.position = transform.position + cameraPosOffset;
    }
}
