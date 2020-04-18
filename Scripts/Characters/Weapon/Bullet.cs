using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Remote remote;
    public GameObject hitEffect;
    public Transform bandolier;
    private void Update()
    {
        if((transform.position - remote.transform.position).magnitude >= remote.GetScope())
        {
            ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
            ObjectPool.GetInstance().RecycleObj(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (remote.GetIsDead())
            return;
        if (remote.GetCharacter(Character.Player)){
            if (collision.transform.tag == "enemy")
            {
                if(remote.transform.name== "PlayerS686")
                {
                    collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - PlayerS686.instance.transform.position).normalized*25);
                }
                ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
                collision.GetComponent<Characters>().PhysicalDamage(remote.GetAtk());
                ObjectPool.GetInstance().RecycleObj(gameObject);
            }
            else if (collision.transform.tag != "Player" && collision.transform.tag != "buttet")
            {
                ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
                ObjectPool.GetInstance().RecycleObj(gameObject);
            }
        }
        else if (remote.GetCharacter(Character.Enemy))
        {
            if (collision.transform.tag == "Player")
            {
                ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
                collision.GetComponent<Characters>().PhysicalDamage(remote.GetAtk());
                ObjectPool.GetInstance().RecycleObj(gameObject);
            }
            else if(collision.transform.tag == "home") 
            {
                ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
                collision.GetComponent<BaseHome>().HealthDelay(-remote.GetAtk());
                ObjectPool.GetInstance().RecycleObj(gameObject);
            }
            else if (collision.transform.tag != "enemy" && collision.transform.tag != "buttet")
            {
                ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
                ObjectPool.GetInstance().RecycleObj(gameObject);
            }
        }
    }
}
