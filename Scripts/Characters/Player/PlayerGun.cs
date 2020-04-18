using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGun : Remote
{
    public static PlayerGun instance;
    //攻击方向
    public Vector2 target;
    //移动方向
    public Vector2 movement;
    
    public Vector2 dashTarget;
    //public GameObject go;
    [Header("冲刺属性")]
    public float dashTime;
    public bool isDashing = false;
    [Header("激光属性")]
    public bool islasing = false;
    [Header("火力全开属性")]
    public bool isFire = false;

    public Image dashCDImage;
    public Image lasingCDImage;
    public Image openFireCDImage;

    public bool isAttack;

    private float timeVal;
    private float timeVal2;

    
    public float CD1;
    public float CD2;
    public float CD3;

    private float dashCD = 3f;
    private float dashSpeed;

    private float lasingCD = 3f;
    private float lasingDamage;

    private float openFireCD = 15f;
    private float openFireTime;
    private float openFireAttackSpeed;
    private float openFireAttackSight;
    private float openFireAttackScope;

    private MaterialTintColor gun;
    private SpriteRenderer tail;
    private Vector2 nowLook = Vector2.zero;
    float dashtime = 0;
    private void Awake()
    {
        instance = this;
        SetCharacter(Character.Player);


        isAttack = false;



        moveSpeed = tunning.instance.playerGun_moveSpeed;
        attackSpeed = tunning.instance.playerGun_attackSpeed;
        Atk = tunning.instance.playerGun_Atk;
        defence = tunning.instance.playerGun_defence;
        magicPower = tunning.instance.playerGun_magicPower;
        magicDefence = tunning.instance.playerGun_magicDefence;
        SetMaxHP(tunning.instance.playerGun_maxHP);
        SetMaxMP(tunning.instance.playerGun_maxMP);
        bulletForce = tunning.instance.playerGun_bulletForce;
        sight = tunning.instance.playerGun_sight;
        scope = tunning.instance.playerGun_scope;

        //moveSpeed = 2.5f;
        //attackSpeed = 1;
        //Atk = 1;
        //defence = 1;
        //magicPower = 1;
        //magicDefence = 1;
        //SetMaxHP(1);
        //SetMaxMP(1);
        //bulletForce =7.5f;
        //sight = 10;
        //scope = 7.5f;


        gun =transform.Find("Weapon").Find("Gun").GetComponent<MaterialTintColor>();
        gun.SetMaterial(transform.Find("Weapon").Find("Gun").GetComponent<SpriteRenderer>().material);
        materialTintColor = GetComponent<MaterialTintColor>();
        materialTintColor.SetMaterial(transform.Find("body").GetComponent<SpriteRenderer>().material);
        firePoint = transform.Find("Weapon").Find("FirePoint").transform;
        bulletPrefab = tunning.instance.playerGun_bulletPrefab;
        //bulletPrefab = go;
        characterRb = GetComponent<Rigidbody2D>();
        characterAnima = GetComponent<Animator>();
        body = transform.Find("body").GetComponent<SpriteRenderer>();
        tail= transform.Find("tail").GetComponent<SpriteRenderer>();
        weaponTrans = transform.Find("Weapon").GetComponent<Transform>();
        leftHand = transform.Find("left_hand").GetComponent<Transform>();
        rightHand = transform.Find("right_hand").GetComponent<Transform>();
        timeVal = 0;

        HpFill = transform.Find("UI").Find("bloodbar").GetComponent<Image>();
        MpFill = transform.Find("UI").Find("magicbar").GetComponent<Image>();
    }
    void Update()
    {
        CD();
        //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (stop)
            Stop();
        Anim();
        Dash();
        if (isDashing || dead)
            return;
        Attack();
        OpenFire();
        Movement();
    }

    private void FixedUpdate()
    {
        if (isDashing || stop || dead)
            return;
        
    }
    protected override void Movement()
    {
        SetLookAt();
        //movement.x = Input.GetAxis("Horizontal");
        //movement.y = Input.GetAxis("Vertical");
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);



        tunning.instance.playerGun_healthPoint = GetHP();
        tunning.instance.playerGun_magicPoint = GetMP();
    }
    private void SetLookAt()
    {
        if (islasing)
            return;
        Vector2 lookDir = target;
        lookDir = lookDir.normalized;
        weaponTrans.right = lookDir;
        if (target.x<0)
        {
            body.flipX = true;
            tail.flipX = true;
            firePoint.localPosition = new Vector3(0.6f, 0.02f, 0);
            weaponTrans.GetComponentInChildren<SpriteRenderer>().flipY = true;
            weaponTrans.position = rightHand.position;
        }
        else if (target.x>0)
        {
            body.flipX = false;
            tail.flipX = false;
            firePoint.localPosition = new Vector3(0.6f, -0.03f, 0);
            weaponTrans.GetComponentInChildren<SpriteRenderer>().flipY = false;
            weaponTrans.position = leftHand.position;
        }
    }
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
    protected override void Attack()
    {
        if (islasing)
            return;
        if (timeVal >= attackSpeed)
        {
            if (isAttack)
            {
                weaponTrans.GetComponentInChildren<Weapon>().Shoot(this);
            }
            timeVal = 0;
        }
        else
            timeVal += Time.deltaTime;
    }
    protected override void Stop()
    {
        base.Stop();
    }

    public void DashButton()
    {
        if (CD1 != 0)
            return;
        if (dashTarget != Vector2.zero)
            nowLook = dashTarget;
        else
            nowLook = movement;

        dashtime = dashTime;
        CD1 = dashCD;
        isDashing = true;
    }
    private void Dash()
    {
        if (stop || dead)
            return;
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    if (movement != Vector2.zero)
        //        nowLook = movement;
        //    else
        //        nowLook = (target - (Vector2)transform.position).normalized;

        //    dashtime = dashTime;
        //    CD1 = dashCD;
        //    isDashing = true;
        //}
        if (isDashing)
        {
            if (dashtime <= 0)
            {
                isDashing = false;
                dashtime = 0;
                characterRb.velocity = Vector2.zero;
                gameObject.layer = LayerMask.NameToLayer("player");
                return;
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("noCol");
                characterRb.velocity = nowLook * moveSpeed * 5;
                dashtime -= Time.deltaTime;
            }
        }
    }
    public void LasingButton()
    {
        if (CD2 != 0)
            return;
        islasing = true;
        characterAnima.Play("PlayerLaser");
        CD2 = lasingCD;
    }
    public void OpenFireButton()
    {
        if (CD3 != 0)
            return;
        attackSpeed = tunning.instance.playerGun_attackSpeed / 2;
        bulletForce = tunning.instance.playerGun_bulletForce * 1.5f;
        sight = tunning.instance.playerGun_sight / 2;
        scope = tunning.instance.playerGun_scope * 2;
        timeVal2 = 10f;
        CD3 = openFireCD;
        isFire = true;
    }
    public void OpenFire()
    {
        if (timeVal2 <= 0)
        {
            attackSpeed = tunning.instance.playerGun_attackSpeed;
            bulletForce = tunning.instance.playerGun_bulletForce;
            sight = tunning.instance.playerGun_sight;
            scope = tunning.instance.playerGun_scope;
            timeVal2 = 0;
            isFire = false;
        }
        else
        {
            timeVal2 -= Time.deltaTime;
        }
        
    }
    public void NotLasing()
    {
        islasing = false;
    }
    private void CD()
    {
        if (CD1 <= 0)
        {
            CD1 = 0;
            dashCDImage.fillAmount = 1;
        }
        else
        {
            CD1 -= Time.deltaTime;
            dashCDImage.fillAmount = (dashCD - CD1) / dashCD;
        }
        if (CD2 <= 0)
        {
            CD2 = 0;
            lasingCDImage.fillAmount = 1;
        }
        else
        {
            CD2 -= Time.deltaTime;
            lasingCDImage.fillAmount = (lasingCD - CD2) / lasingCD;
        }
        if (CD3 <= 0)
        {
            CD3 = 0;
            openFireCDImage.fillAmount = 1;
        }
        else
        {
            CD3 -= Time.deltaTime;
            openFireCDImage.fillAmount = (openFireCD - CD3) / openFireCD;
        }
    }
}
