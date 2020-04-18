using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public Melee melee;
    public float HitStop = 1f;
    private void Start()
    {
        HitStop /= 60;    
    }
    public override void Hack(Melee melee)
    {
        this.melee = melee;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (melee.GetCharacter(Character.Enemy))
        {
            if (collision.transform.tag == "home")
            {
                collision.GetComponent<BaseHome>().HealthDelay(-melee.GetAtk());
                transform.parent.parent.GetComponent<Animator>().speed = 0;
                Invoke("AnimPlay", HitStop);
            }
            else if (collision.transform.tag == "Player")
            {
                collision.GetComponent<Characters>().PhysicalDamage(melee.GetAtk());
                transform.parent.parent.GetComponent<Animator>().speed = 0;
                Invoke("AnimPlay", HitStop);
            }
        }
    }
    private void AnimPlay()
    {
        transform.parent.parent.GetComponent<Animator>().speed = 1;
    }
}
