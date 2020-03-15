using System;
using UnityEngine;

public class EnemyController : IPawn
{
    public GameObject SpawnObjectOnDeath;

    [HideInInspector]
    public GameManager Manager;
    [HideInInspector]
    public Transform Target;
    
    public override void OnBulletHit(int damage)
    {
        MaxHealth -= damage;

        if (MaxHealth <= 0)
        {
            System.Random random = new System.Random();
            
            int numberOfCoinsToSpawn = random.Next(1, 10);
            
            while (numberOfCoinsToSpawn > 0)
            {
                numberOfCoinsToSpawn--;
                
                float randX = (float)random.NextDouble() * random.Next( 1, 3);
                
                float randZ = (float)random.NextDouble() * random.Next( 1, 3);

                Vector3 randPosOffset = new Vector3(randX, 0.0f, randZ);
                
                GameObject spawnedCoin = Instantiate(SpawnObjectOnDeath, 
                                                transform.position + randPosOffset,
                                                transform.rotation
                                            );
                spawnedCoin.GetComponent<CollectableCoin>().Manager = Manager;
                spawnedCoin.GetComponent<CollectableCoin>().CollectionTarget = Target;
            }
            
            Destroy(gameObject);
        }
    }
}
