using UnityEngine;

public class PlayerController : IPawn
{
    public GameManager Manager;
    private TargetingSystem targetingSystem;

    private PlayerMovement playerMovement;

    private WeponMechanic weponMechanic;

    private Rigidbody rigidBody;
    void Start()
    {
        weponMechanic = GetComponent<WeponMechanic>();

        targetingSystem = GetComponent<TargetingSystem>();

        playerMovement = GetComponent<PlayerMovement>();

        rigidBody = GetComponent<Rigidbody>();
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

    public void OnPlayerLevelUp()
    {
        if (!weponMechanic.EnableSideFire)
        {
            weponMechanic.EnableSideFire = true;
        }
        else if (weponMechanic.BurstFireRate < 4)
        {
            weponMechanic.BurstFireRate += 1;
        }
    }

    public void OnPlayerDied()
    {
        weponMechanic.BurstFireRate = 1;
        weponMechanic.EnableSideFire = false;
    }
}