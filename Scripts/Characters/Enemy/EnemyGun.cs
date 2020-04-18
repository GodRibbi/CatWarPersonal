using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGun : Remote
{
    public static EnemyGun instance;
    //move
    public float playerDistance;
    public float minDistance;

    private Transform target;
    private Transform player;
    

    private float timeVal;
    private void Awake()
    {
        SetCharacter(Character.Enemy);
        target = tunning.instance.home;
        player = tunning.instance.player;
        


        moveSpeed = tunning.instance.enemyGun_moveSpeed;
        attackSpeed = tunning.instance.enemyGun_attackSpeed;
        Atk = tunning.instance.enemyGun_Atk;
        defence = tunning.instance.enemyGun_defence;
        magicDefence = tunning.instance.enemyGun_magicDefence;
        SetMaxHP(tunning.instance.enemyGun_maxHP);
        bulletForce = tunning.instance.enemyGun_bulletForce;
        sight = tunning.instance.enemyGun_sight;
        scope = tunning.instance.enemyGun_scope;



        firePoint = transform.Find("Weapon").Find("FirePoint").transform;
        bulletPrefab = tunning.instance.enemyGun_bulletPrefab;
        materialTintColor = GetComponent<MaterialTintColor>();
        materialTintColor.SetMaterial(transform.Find("body").GetComponent<SpriteRenderer>().material);
        characterRb = GetComponent<Rigidbody2D>();
        HpFill = transform.Find("UI").Find("bloodbar").GetComponent<Image>();
        characterAnima = GetComponent<Animator>();
        body = transform.Find("body").GetComponent<SpriteRenderer>();
        weaponTrans = transform.Find("Weapon").GetComponent<Transform>();
        leftHand = transform.Find("left_hand").GetComponent<Transform>();
        rightHand = transform.Find("right_hand").GetComponent<Transform>();
        timeVal = 0;
        instance = this;
    }
    void Update()
    {
        //Anim();
    }
    private void FixedUpdate()
    {
        if (stop)
            Stop();
        Movement();
    }
    protected override void Movement()
    {
        if (dead || BaseHome.GameOver || stop)
            return;
        if (checkDistance(player, playerDistance))
        {
            if (!checkDistance(player, minDistance))
            {
                Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
                //characterRb.AddForce(direction.normalized * Time.deltaTime * moveSpeed);
                transform.Translate(direction.normalized * Time.deltaTime * moveSpeed);
            }
            else
            {
                Attack();
            }
            SetLookAt(player);
            Vector2 lookDir = (Vector2)player.position - (Vector2)weaponTrans.position;
            lookDir = lookDir.normalized;
            weaponTrans.right = lookDir;
        }
        else if (!checkDistance(target, minDistance * 1.5f))
        {
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * moveSpeed);
        }
        else
        {
            SetLookAt(target);
            Vector2 lookDir = (Vector2)target.position - (Vector2)weaponTrans.position;
            lookDir = lookDir.normalized;
            weaponTrans.right = lookDir;
            Attack();
        }
            

        tunning.instance.enemyGun_healthPoint = GetHP();
    }
    private bool checkDistance(Transform target, float distance)
    {
        if (target == null)
            return false;
        float thisdistance = (target.position - transform.position).magnitude;
        if (thisdistance <= distance)
        {
            return true;
        }
        return false;
    }
    void SetLookAt(Transform target)
    {
        if (transform.position.x > target.position.x)
        {
            body.flipX = true;
            weaponTrans.GetComponentInChildren<SpriteRenderer>().flipY = true;
            weaponTrans.position = rightHand.position;
        }
        else if (transform.position.x < target.position.x)
        {
            body.flipX = false;
            weaponTrans.GetComponentInChildren<SpriteRenderer>().flipY = false;
            weaponTrans.position = leftHand.position;
        }
    }
    protected override void Attack()
    {
        if (timeVal >= attackSpeed)
        {
            timeVal = 0;
            weaponTrans.GetComponentInChildren<Weapon>().Shoot(this);
        }
        else
            timeVal += Time.deltaTime;

    }
    protected override void Stop()
    {
        base.Stop();
    }
}
