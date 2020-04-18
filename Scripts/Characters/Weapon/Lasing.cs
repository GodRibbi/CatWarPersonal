using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasing : Weapon
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            collision.GetComponent<Characters>().PhysicalDamage(PlayerGun.instance.GetAtk()* 2,1f);
        }
    }
}
