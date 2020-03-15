using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    [HideInInspector]
    public Transform CollectionTarget;
    
    [HideInInspector]
    public GameManager Manager;

    public int CoinCollectSpeed = 20;

    public int CoinValue = 10;
    void Update()
    {
        switch (Manager.ActiveGameState)
        {
            case GameManager.EGameState.InLevel:
                transform.Rotate(new Vector3(0,0.1f,0));
            break;

            case GameManager.EGameState.LevelCleared:
                float stride = CoinCollectSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, CollectionTarget.position, stride);

                if (Vector3.Distance(transform.position, CollectionTarget.position) < 0.1)
                {
                    Manager.CurrentScore += CoinValue;
                    Destroy(gameObject);
                }
            break;
        }
    }
}
