using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject InGameScoreBoard;

    public Text CurrentScoreText;

    private int previousScore = -1;

    [HideInInspector]
    public int CurrentScore = 0;

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
                
                GameObject spawnedEvemy = Instantiate(EnemyPrefab, SpawnPoints[currentPos].SpawnPoint.position, SpawnPoints[currentPos].SpawnPoint.rotation);
                
                spawnedEvemy.GetComponent<EnemyController>().Manager = this;

                spawnedEvemy.GetComponent<EnemyController>().Target = PlayerObject.gameObject.transform;

                spawnCount++;
            }
        }
    }

    void Update()
    {
        if (CurrentScore != previousScore)
        {
            previousScore = CurrentScore;

            CurrentScoreText.text = $"Current Score: {previousScore}";
        }

        switch (ActiveGameState)
        {
            case EGameState.LevelFail:
                LevelFailedMenu.SetActive(true);
                InGameScoreBoard.SetActive(false);
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

                ResetTargets();

                ActiveGameState = EGameState.InLevel;

                ++CurrentLevel;

                break;
            case EGameState.LevelClearMenu:
                
                PlayerObject.gameObject.SetActive(false);

                LevelClearMenu.SetActive(true);

                break;

            case EGameState.InLevel:
                
                InGameScoreBoard.SetActive(true);

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

        ResetTargets();

        PlayerObject.gameObject.SetActive(true);

        PlayerObject.transform.position = PlayerSpawnPoint.position;

        ActiveGameState = EGameState.InLevel;
    }

    public void StartNextLevel()
    {
        PlayerObject.GetComponent<PlayerController>().OnPlayerLevelUp();
        
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

        CurrentScore = 0;
        
        previousScore = -1;

        PlayerObject.GetComponent<PlayerController>().OnPlayerLevelUp();

        var spawnedCoins = GameObject.FindGameObjectsWithTag("Props");

        foreach(GameObject coin in spawnedCoins)
        {
            Destroy(coin.gameObject);
        }
    }

    public void QuitGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
#endif        
    }

    public void ResetTargets()
    {
        PlayerObject.GetComponent<TargetingSystem>().RefreshTargets();
    }
}
