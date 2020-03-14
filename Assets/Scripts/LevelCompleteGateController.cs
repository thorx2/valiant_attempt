using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteGateController : MonoBehaviour
{
    public GameManager Manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Manager.ActiveGameState = GameManager.EGameState.LevelClearMenu;
        }
    }
}
