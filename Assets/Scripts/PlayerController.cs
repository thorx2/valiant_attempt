using UnityEngine;

public class PlayerController : IPawn
{
    public GameManager Manager;
    private TargetingSystem targetingSystem;

    private PlayerMovement playerMovement;

    private WeponMechanic weponMechanic;

    void Start()
    {
        weponMechanic = GetComponent<WeponMechanic>();

        targetingSystem = GetComponent<TargetingSystem>();

        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!playerMovement.IsMoving &&
            MaxHealth > 0)
        {
            weponMechanic.StartFiring();
        }

        if (playerMovement.IsMoving || targetingSystem.NearestTarget == null)
        {
            weponMechanic.StopFiring();

            if (Manager.ActiveGameState == GameManager.EGameState.InLevel && targetingSystem.NearestTarget == null)
            {
                Manager.ActiveGameState = GameManager.EGameState.LevelCleared;
            }
        }

        if (!targetingSystem.AllTargetsDestroyed)
        {
            Manager.ActiveGameState = GameManager.EGameState.InLevel;
        }
    }

    public override void OnBulletHit(int damage)
    {
        MaxHealth -= damage;

        if (MaxHealth <= 0)
        {
            Manager.ActiveGameState = GameManager.EGameState.LevelFail;

            gameObject.SetActive(false);
        }
    }
}