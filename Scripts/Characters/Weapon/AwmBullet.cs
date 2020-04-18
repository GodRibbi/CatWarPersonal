using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwmBullet : Bullet
{
    public bool isPenetrate;
    private void Update()
    {
        if ((transform.position - remote.transform.position).magnitude >= remote.GetScope())
        { 
            ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
            ObjectPool.GetInstance().RecycleObj(gameObject);
            isPenetrate = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (remote.GetIsDead())
            return;
        if (collision.transform.tag == "enemy")
        {
            ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
            collision.GetComponent<Characters>().PhysicalDamage(remote.GetAtk()*20);
            Debug.Log(isPenetrate);
            if (!isPenetrate)
                ObjectPool.GetInstance().RecycleObj(gameObject);
        }
        else if (collision.transform.tag != "Player" && collision.transform.tag != "buttet")
        {
            ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
            ObjectPool.GetInstance().RecycleObj(gameObject);
            isPenetrate = false;
        }
    }
}
