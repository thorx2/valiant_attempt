using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public int RoundSpeed = 20;
    bool hasBeenFired = false;
    private Vector3 targetPosition;

    [HideInInspector]
    public int DamageOfRound;
    // Update is called once per frame
    void Update()
    {
        float stride = RoundSpeed * Time.deltaTime;

        if (hasBeenFired)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, stride);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1)
        {
            Destroy(gameObject);
        }
    }

    public void LaunchTowards(Vector3 target)
    {
        targetPosition = target;

        hasBeenFired = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arena")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            // Take damage
        }
    }
}
