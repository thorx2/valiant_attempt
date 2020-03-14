﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponMechanic : MonoBehaviour
{

    [Tooltip("The rate of fire of the weapon in seconds.")]
    [Range(0.1f, 2.0f)]
    public float RateOfFire = 1.0f;

    public int DamagePerHit;

    [Range(1, 20)]
    public int Range = 1;

    public Ammunition Ammo;

    private IEnumerator coroutine;

    private bool isFiring = false;

    public bool IsStationaryTurret = true;

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
            coroutine = FireAction();
            
            isFiring = true;

            StartCoroutine(coroutine);
        }
    }

    public void StopFiring()
    {
        isFiring = false;
        StopCoroutine(coroutine);
    }


    private IEnumerator FireAction()
    {
        while(isFiring)
        {
            yield return new WaitForSeconds (RateOfFire);

            Ammunition ammo = Instantiate(Ammo, transform.position + (transform.forward), transform.rotation);

            ammo.DamageOfRound = DamagePerHit;
            
            ammo.LaunchTowards(transform.position + (transform.forward * Range));
        }
    }
}
