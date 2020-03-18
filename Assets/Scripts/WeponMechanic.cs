using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponMechanic : MonoBehaviour
{

    [Tooltip("The rate of fire of the weapon in seconds.")]
    [Range(0.5f, 2.0f)]
    public float RateOfFire = 1.0f;

    public int DamagePerHit;

    [Range(1, 20)]
    public int Range = 1;

    public Ammunition Ammo;

    private IEnumerator coroutine;

    private bool isFiring = false;

    public bool IsStationaryTurret = true;

    [Tooltip("Number of bullets fired simultaniously on each trigger pull")]
    public int BurstFireRate = 1;

    public bool EnableSideFire = false;

    void Start()
    {
        if (IsStationaryTurret)
        {
            StartFiring();
        }
    }

    public void StartFiring()
    {
        if (!isFiring)
        {
            coroutine = TriggerAction();
            
            isFiring = true;

            StartCoroutine(coroutine);
        }
    }

    public void StopFiring()
    {
        isFiring = false;
        StopCoroutine(coroutine);
    }


    private IEnumerator TriggerAction()
    {
        while(isFiring)
        {
            yield return new WaitForSeconds (RateOfFire);

            Coroutine burstFire = StartCoroutine(BurstFireBulletGroups());

            yield return burstFire;
        }
    }

    private IEnumerator BurstFireBulletGroups()
    {
        int bulletsFired = 0;

        while (bulletsFired < BurstFireRate)
        {
            bulletsFired++;

            yield return new WaitForSeconds (0.1f);

            Ammunition ammo = Instantiate(Ammo, transform.position + (transform.forward), transform.rotation);

            ammo.DamageOfRound = DamagePerHit;
            
            ammo.LaunchTowards(transform.position + (transform.forward * Range));

            if (EnableSideFire)
            {
                Ammunition rightShot = Instantiate(Ammo, transform.position + (transform.right), transform.rotation);

                rightShot.DamageOfRound = DamagePerHit;

                rightShot.LaunchTowards(transform.position + (transform.right * Range));

                Ammunition leftShot = Instantiate(Ammo, transform.position + (-transform.right), transform.rotation);

                leftShot.DamageOfRound = DamagePerHit;
                
                leftShot.LaunchTowards(transform.position + (-transform.right * Range));
            }
        }
    }
}
