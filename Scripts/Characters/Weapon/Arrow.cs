using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Remote remote;
    public float timeVal;
    public bool launch;
    private float distance;
    private float damage;
    private void Start()
    {
        launch = false;
    }
    private void Update()
    {
        if (!launch)
            return;
        if (timeVal < remote.GetAttackSpeed())
        {
            distance = remote.GetScope() / (2 - Percent.GetPercent(timeVal, remote.GetAttackSpeed()));
            damage = remote.GetAtk() * Percent.GetPercent(timeVal, remote.GetAttackSpeed()) * 5;
        }
        else if (timeVal >= remote.GetAttackSpeed() && timeVal < remote.GetAttackSpeed() * 2)
        {
            distance = remote.GetScope() * Percent.GetPercent(timeVal, remote.GetAttackSpeed());
            damage = remote.GetAtk() * Percent.GetPercent(timeVal, remote.GetAttackSpeed()) * 5;
        }
        else if (timeVal >= remote.GetAttackSpeed() * 2)
        {
            distance = remote.GetScope() * 2;
            damage = remote.GetAtk() * 10;
        }
        if ((transform.position - remote.transform.position).magnitude >= distance)
        {
            //ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
            ObjectPool.GetInstance().RecycleObj(gameObject);
            launch = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (remote.GetIsDead())
            return;
        if (remote.GetCharacter(Character.Player))
        {
            if (collision.transform.tag == "enemy")
            {
                //ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
                collision.GetComponent<Characters>().PhysicalDamage(damage);
                ObjectPool.GetInstance().RecycleObj(gameObject);
                launch = false;
            }
            else if (collision.transform.tag != "Player" && collision.transform.tag != "buttet")
            {
                //ObjectPool.GetInstance().GetObj(hitEffect, transform.position, Quaternion.identity, bandolier);
                ObjectPool.GetInstance().RecycleObj(gameObject);
                launch = false;
            }
        }
    }
}
