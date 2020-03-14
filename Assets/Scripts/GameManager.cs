using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum EGameState
    {
        GameMenu,
        BoostMenu,
        LevelComplete,
        LevelFail
    }

    public SpawnPointModel[] SpawnPoints;

    public EGameState ActiveGameState = EGameState.GameMenu;
    int CurrentLevel = 1;

    public GameObject EnemyPrefab;

    public GameObject PlayerObject;

    public Transform PlayerSpawnPoint;
    
    void Start()
    {
        SpawnEnemies();

        PlayerObject.transform.position = PlayerSpawnPoint.position;
    }

    void SpawnEnemies()
    {
        int spawnCount = 0;
        while(spawnCount != CurrentLevel + 1)
        {
            System.Random randGen = new System.Random();
            int currentPos = randGen.Next(1, 8);
            if (!SpawnPoints[currentPos].IsUsed)
            {
                Instantiate(EnemyPrefab, SpawnPoints[currentPos].SpawnPoint.position, SpawnPoints[currentPos].SpawnPoint.rotation);
                spawnCount++;
            }
        }
    }

    void Update()
    {
        switch (ActiveGameState)
        {
            case EGameState.LevelFail:
            
                break;

            case EGameState.LevelComplete:
                
                foreach(var spawnPoint in SpawnPoints)
                {
                    spawnPoint.IsUsed = false;
                }

                ++CurrentLevel;

                break;

            default:
                break;
        }
    }
}
