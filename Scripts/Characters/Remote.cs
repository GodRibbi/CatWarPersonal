using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : Characters
{
    protected SpriteRenderer body;
    protected Transform leftHand;
    protected Transform rightHand;

    protected Transform firePoint;
    protected GameObject bulletPrefab;
    /// <summary>
    /// 子弹速度
    /// </summary>
    protected float bulletForce;
    /// <summary>
    /// 准星
    /// </summary>
    protected int sight;
    /// <summary>
    /// 射程
    /// </summary>
    protected float scope;

    public void SetFirePoint(Transform firePoint)
    {
        this.firePoint = firePoint;
    }
    public void SetBulletPrefab(GameObject bulletPrefab)
    {
        this.bulletPrefab = bulletPrefab;
    }
    public void SetBulletForce(float bulletForce)
    {
        this.bulletForce = bulletForce;
    }
    public void SetSight(int sight)
    {
        this.sight = sight;
    }
    public void SetScope(float scope)
    {
        this.scope = scope;
    }
    public Transform GetFirePoint()
    {
        return firePoint;
    }
    public GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }
    public float GetBulletForce()
    {
        return bulletForce;
    }
    public int GetSight()
    {
        return sight;
    }
    public float GetScope()
    {
        return scope;
    }
}
