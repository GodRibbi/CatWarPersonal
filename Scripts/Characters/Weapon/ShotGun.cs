using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    private Transform bandolier;
    private void Start()
    {
        bandolier = GameObject.FindGameObjectWithTag("bandolier").transform;
    }
    public override void ShotGunShoot(Remote remote)
    {
        //准星效果
        for (int i = 0; i < 15; i++)
        {
            GameObject bullet = ObjectPool.GetInstance().GetObj(remote.GetBulletPrefab(),
            remote.GetFirePoint().position,
            remote.GetFirePoint().rotation *
            Quaternion.AngleAxis(Random.Range(-remote.GetSight(), remote.GetSight()), Vector3.forward),
            bandolier);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * remote.GetBulletForce(), ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().remote = remote;
            bullet.GetComponent<Bullet>().bandolier = bandolier;
        }
        //射击动画效果
        //remote.GetComponent<Animator>().Play("GunShoot");
        effect.Play("GE");
    }
}
