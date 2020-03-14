using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : IPawn
{
    public override void OnBulletHit(int damage)
    {
        MaxHealth -= damage;

        if (MaxHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
