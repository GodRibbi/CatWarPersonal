using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    private Transform bandolier;
    private void Start()
    {
        bandolier = GameObject.FindGameObjectWithTag("bandolier").transform;
    }
    public override void Archery(Remote remote, float timeVal, GameObject arrow)
    {
        if (timeVal < remote.GetAttackSpeed())
        {
            arrow.GetComponent<Rigidbody2D>().AddForce(
                arrow.transform.right * remote.GetBulletForce()/ (2 - Percent.GetPercent(timeVal, remote.GetAttackSpeed())), ForceMode2D.Impulse);
        }
        else if(timeVal>= remote.GetAttackSpeed()&& timeVal < remote.GetAttackSpeed() * 2)
        {
            arrow.GetComponent<Rigidbody2D>().AddForce(
                arrow.transform.right * remote.GetBulletForce() * Percent.GetPercent(timeVal, remote.GetAttackSpeed()), ForceMode2D.Impulse);
        }
        else if(timeVal >= remote.GetAttackSpeed() * 2)
        {
            arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * remote.GetBulletForce() * 2, ForceMode2D.Impulse);
        }
        arrow.transform.SetParent(bandolier);
        arrow.GetComponent<Arrow>().remote = remote;
        arrow.GetComponent<Arrow>().timeVal = timeVal;
        arrow.GetComponent<Arrow>().launch = true;
    }
}
