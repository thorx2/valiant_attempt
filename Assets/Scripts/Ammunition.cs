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

    private void OnTriggerEnter(Collider other)
    {    
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            other.gameObject.GetComponent<IPawn>().OnBulletHit(DamageOfRound);
        }
        else if (other.gameObject.tag == "Arena")
        {
            Destroy(gameObject);
        }
    }
}
