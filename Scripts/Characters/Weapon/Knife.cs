using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Remote remote;
    private void Update()
    {
        if ((transform.position - remote.transform.position).magnitude >= remote.GetScope())
        {
            ObjectPool.GetInstance().RecycleObj(gameObject);
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
                collision.GetComponent<Characters>().PhysicalDamage(remote.GetAtk()*3);
                ObjectPool.GetInstance().RecycleObj(gameObject);
            }
            else if (collision.transform.tag != "Player" && collision.transform.tag != "buttet")
            {
                ObjectPool.GetInstance().RecycleObj(gameObject);
            }
        }
    }
}
