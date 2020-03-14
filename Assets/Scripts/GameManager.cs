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
        InLevel,
        LevelCleared,
        LevelClearMenu,
        LevelComplete,
        LevelFail
    }

    public SpawnPointModel[] SpawnPoints;

    public EGameState ActiveGameState;
    int CurrentLevel = 1;

    public GameObject EnemyPrefab;

    public IPawn PlayerObject;

    public Transform PlayerSpawnPoint;

    public GameObject LevelCompleteGate;
    
    public GameObject LevelFailedMenu;

    public GameObject MainMenu;

    public GameObject LevelClearMenu;

    void Start()
    {
        MainMenu.SetActive(true);

        ActiveGameState = EGameState.GameMenu;

        LevelCompleteGate.SetActive(false);

        PlayerObject.gameObject.SetActive(false);
    }

    void SpawnEnemies()
    {
        int spawnCount = 0;
        while(spawnCount < CurrentLevel + 1)
        {
            System.Random randGen = new System.Random();
            
            int currentPos = randGen.Next(1, 8);
            
            if (!SpawnPoints[currentPos].IsUsed)
            {
                SpawnPoints[currentPos].IsUsed = true;
                
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
                LevelFailedMenu.SetActive(true);
                break;

            case EGameState.LevelCleared:
                
                LevelCompleteGate.SetActive(true);
                
                break;

            case EGameState.LevelComplete:
                
                PlayerObject.gameObject.SetActive(true);
                
                LevelCompleteGate.SetActive(false);

                PlayerObject.transform.position = PlayerSpawnPoint.position;

                foreach(var spawnPoint in SpawnPoints)
                {
                    spawnPoint.IsUsed = false;
                }

                SpawnEnemies();

                ActiveGameState = EGameState.InLevel;

                ++CurrentLevel;

                break;
            case EGameState.LevelClearMenu:
                
                PlayerObject.gameObject.SetActive(false);

                LevelClearMenu.SetActive(true);

                break;

            case EGameState.InLevel:
                
                LevelCompleteGate.SetActive(false);

                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        MainMenu.SetActive(false);

        SpawnEnemies();

        PlayerObject.gameObject.SetActive(true);

        PlayerObject.transform.position = PlayerSpawnPoint.position;

        ActiveGameState = EGameState.InLevel;
    }

    public void StartNextLevel()
    {
        LevelClearMenu.SetActive(false);

        ActiveGameState = EGameState.LevelComplete;
    }
    
    public void RestartGame()
    {
        var enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemiesLeft)
        {
            Destroy(enemy.gameObject);
        }

        LevelFailedMenu.SetActive(false);

        PlayerObject.MaxHealth = 100;

        CurrentLevel = 1;

        ActiveGameState = EGameState.LevelComplete;
    }

    public void QuitGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
#endif        
    }
}
