using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySword : Melee
{
    public static EnemySword instance;
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

        moveSpeed = tunning.instance.enemySword_moveSpeed;
        attackSpeed = tunning.instance.enemySword_attackSpeed;
        Atk = tunning.instance.enemySword_Atk;
        defence = tunning.instance.enemySword_defence;
        magicDefence = tunning.instance.enemySword_magicDefence;
        SetMaxHP(tunning.instance.enemySword_maxHP);


        materialTintColor = GetComponent<MaterialTintColor>();
        materialTintColor.SetMaterial(transform.Find("body").GetComponent<SpriteRenderer>().material);
        characterRb = GetComponent<Rigidbody2D>();
        weaponTrans = transform.Find("Weapon").GetComponent<Transform>();
        HpFill = transform.Find("UI").Find("bloodbar").GetComponent<Image>();
        characterAnima = GetComponent<Animator>();
        instance = this;
    }
    void Update()
    {
        Anim();
        if (stop)
            Stop();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    /// <summary>
    /// 移动
    /// </summary>
    protected override void Movement()
    {
        if (dead || BaseHome.GameOver || stop)
            return;
        if (checkDistance(player, playerDistance))
        {
            if(!checkDistance(player, minDistance))
            {
                SetLookAt(player);
                Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
                transform.Translate(direction.normalized * Time.deltaTime * moveSpeed);
            }
            else
                Attack();
        }
        else if (!checkDistance(target, minDistance*1.5f))
        {
            SetLookAt(target);
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * moveSpeed);
        }
        else
            Attack();

        tunning.instance.enemySword_healthPoint = GetHP();
    }
    /// <summary>
    /// 确定自身与目标之间的距离是否小于某个值
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="distance">值</param>
    /// <returns></returns>
    private bool checkDistance(Transform target,float distance)
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
    /// <summary>
    /// 身体转向
    /// </summary>
    void SetLookAt(Transform target)
    {
        if (transform.position.x > target.position.x)
        {
            HpFill.fillOrigin = 1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x < target.position.x)
        {
            HpFill.fillOrigin = 0;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    /// <summary>
    /// 角色动画
    /// </summary>
    protected override void Anim()
    {
        //if (movement == new Vector2(0, 0))
        //{
        //    playerAnima.SetBool("walk", false);
        //}
        //else
        //{
        //    playerAnima.SetBool("walk", true);
        //}
    }
    /// <summary>
    /// 攻击
    /// </summary>
    protected override void Attack()
    {
        if (timeVal >= attackSpeed)
        {
            timeVal = 0;
            characterAnima.Play("attack");
            weaponTrans.GetComponentInChildren<Weapon>().Hack(this);
        }
        else
            timeVal += Time.deltaTime;
        
    }
    protected override void Stop()
    {
        base.Stop();
    }
}
