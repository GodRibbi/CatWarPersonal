using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    private Transform bandolier;
    private void Start()
    {
        bandolier = GameObject.FindGameObjectWithTag("bandolier").transform;
    }
    public override void Shoot(Remote remote)
    {
        //准星效果
        GameObject bullet = ObjectPool.GetInstance().GetObj(remote.GetBulletPrefab(),
            remote.GetFirePoint().position,
            remote.GetFirePoint().rotation * 
            Quaternion.AngleAxis(Random.Range(-remote.GetSight(),remote.GetSight()), Vector3.forward),
            bandolier);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * remote.GetBulletForce(), ForceMode2D.Impulse);
        bullet.GetComponent<Bullet>().remote = remote;
        bullet.GetComponent<Bullet>().bandolier = bandolier;
        if (bullet.GetComponent<AwmBullet>() != null)
        {
            if (PlayerAwm.instance.isPenetrate)
            {
                Debug.Log(PlayerAwm.instance.isPenetrate);
                bullet.GetComponent<AwmBullet>().isPenetrate = true;
                PlayerAwm.instance.isPenetrate = false;
            }
        }
        //射击动画效果
        //remote.GetComponent<Animator>().Play("GunShoot");
        effect.Play("GE");
    }
}
