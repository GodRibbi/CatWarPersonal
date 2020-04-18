using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Vector2 targer;
    public GameObject explorion;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targer, 4f * Time.deltaTime);
        if (transform.position == (Vector3)targer) 
        {
            Destroy();
        }
    }
    void Destroy()
    {
        GameObject grenades = ObjectPool.GetInstance().GetObj(explorion, transform.position);
        foreach(GameObject go in SpawnMonster.instance.monsterList)
        {
            if (Vector3.Distance(go.transform.position, transform.position) <= 2)
            {
                go.GetComponent<Characters>().PhysicalDamage(PlayerS686.instance.GetAtk()*5);
                go.GetComponent<Characters>().GetRb().AddForce((go.transform.position - transform.position).normalized*125f);
            }
        }
        ObjectPool.GetInstance().RecycleObj(gameObject);
    }
}
