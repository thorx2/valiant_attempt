using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPawn : MonoBehaviour
{
    public int MaxHealth;

    public abstract void OnBulletHit(int damage);
}
