using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator effect;
    public virtual void Shoot(Remote remote)
    {
        
    }
    public virtual void ShotGunShoot(Remote remote)
    {

    }
    public virtual void Hack(Melee melee)
    {

    }

    public virtual void Archery(Remote remote, float timeVal,GameObject arrow)
    {
        
    }
}
