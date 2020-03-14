using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 cameraPosOffset;

    public float Speed;
    public Joystick joycon;
    public bool IsMoving = false;

    private TargetingSystem playerTargettingSystem;

    void Start()
    {
        playerTargettingSystem = GetComponent<TargetingSystem>();

        cameraPosOffset = Camera.main.transform.position - transform.position;
    }

    
    void Update()
    {
        float stride = Speed * Time.deltaTime;
        
        if (joycon.Horizontal != 0 || joycon.Vertical != 0)
        {
            Vector3 joyconDirection = new Vector3 (transform.position.x + joycon.Horizontal, 0.5f, transform.position.z + joycon.Vertical);

            transform.LookAt (joyconDirection);

            Quaternion rot = Quaternion.Euler( 0, transform.rotation.eulerAngles.y, 0 );

            transform.rotation = rot;

            transform.position = Vector3.MoveTowards (transform.position, joyconDirection, stride);

            IsMoving = true;

            playerTargettingSystem.StopTargetAcquisition = true;
        }
        else
        {
            playerTargettingSystem.StopTargetAcquisition = false;
            IsMoving = false;
        }
    }

    void LateUpdate()
    {
        Camera.main.transform.position = transform.position + cameraPosOffset;
    }
}
