using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAwm : Remote
{
    public static PlayerAwm instance;
    //攻击方向
    public Vector2 target;
    //移动方向
    public Vector2 movement;
    public Camera mainCamera;
    public GameObject awmMod;
    public GameObject normalMod;
    public Transform redDot;
    [Header("转换属性")]
    public bool isTransform = false;
    [Header("红点属性")]
    public bool isRedDot = false;
    [Header("穿透属性")]
    public bool isPenetrate = false;

    public Image penetrateCDImage;
    public Image AwmAttackCDImage;

    public bool isAttack;

    private float timeVal;

    public float CD1;

    private float penetrateCD = 10f;

    private MaterialTintColor gun;
    private SpriteRenderer tail;
    private void Awake()
    {
        instance = this;
        SetCharacter(Character.Player);


        isAttack = false;


        moveSpeed = tunning.instance.playerAwm_moveSpeed;
        attackSpeed = tunning.instance.playerAwm_attackSpeed;
        Atk = tunning.instance.playerAwm_Atk;
        defence = tunning.instance.playerAwm_defence;
        magicPower = tunning.instance.playerAwm_magicPower;
        magicDefence = tunning.instance.playerAwm_magicDefence;
        SetMaxHP(tunning.instance.playerAwm_maxHP);
        SetMaxMP(tunning.instance.playerAwm_maxMP);
        bulletForce = tunning.instance.playerAwm_bulletForce;
        sight = tunning.instance.playerAwm_sight;
        scope = tunning.instance.playerAwm_scope;

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


        //gun = transform.Find("Weapon").Find("Gun").GetComponent<MaterialTintColor>();
        //gun.SetMaterial(transform.Find("Weapon").Find("Gun").GetComponent<SpriteRenderer>().material);
        materialTintColor = GetComponent<MaterialTintColor>();
        materialTintColor.SetMaterial(transform.Find("body").GetComponent<SpriteRenderer>().material);
        firePoint = transform.Find("Weapon").Find("Second").Find("FirePoint").transform;
        bulletPrefab = tunning.instance.playerGun_bulletPrefab;
        //bulletPrefab = go;
        characterRb = GetComponent<Rigidbody2D>();
        characterAnima = GetComponent<Animator>();
        body = transform.Find("body").GetComponent<SpriteRenderer>();
        tail = transform.Find("tail").GetComponent<SpriteRenderer>();
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
        if (dead || stop)
            return;
        if (isTransform)
            AwmAttack();
        else
            Attack();
        Movement();
    }

    private void FixedUpdate()
    {
        if (stop || dead)
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
        Vector2 lookDir = target;
        lookDir = lookDir.normalized;
        weaponTrans.right = lookDir;
        if (target.x < 0)
        {
            
            body.flipX = true;
            tail.flipX = true;
            
            if (isTransform)
            {
                redDot.localPosition = new Vector3(0.461f, -0.12f, 0);
                weaponTrans.Find("First").Find("FirePoint").localPosition = new Vector3(1.12f, -0.04f, 0);
            }
            else
                weaponTrans.Find("Second").Find("FirePoint").localPosition = new Vector3(0.28f, -0.07f, 0);
            weaponTrans.GetComponentInChildren<SpriteRenderer>().flipY = true;
            weaponTrans.position = rightHand.position;
        }
        else if (target.x > 0)
        {
            
            body.flipX = false;
            tail.flipX = false;
            
            if (isTransform)
            {
                weaponTrans.Find("First").Find("FirePoint").localPosition = new Vector3(1.12f, 0.04f, 0);
                redDot.localPosition = new Vector3(0.461f, 0.12f, 0);
            }
            else
                weaponTrans.Find("Second").Find("FirePoint").localPosition = new Vector3(0.28f, 0.08f, 0);
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
    private void CD()
    {
        if (CD1 <= 0)
        {
            CD1 = 0;
            penetrateCDImage.fillAmount = 1;
        }
        else
        {
            CD1 -= Time.deltaTime;
            penetrateCDImage.fillAmount = (penetrateCD - CD1) / penetrateCD;
        }
    }
    protected override void Stop()
    {
        base.Stop();
    }





    private void AwmAttack()
    {
        if (timeVal >= attackSpeed)
        {
            AwmAttackCDImage.fillAmount = 1;
            if (isAttack && !isPenetrate)
            {
                weaponTrans.GetComponentInChildren<Weapon>().Shoot(this);
                isAttack = false;
                timeVal = 0;
            }
            else if (isAttack && isPenetrate)
            {
                CD1 = penetrateCD;
                weaponTrans.GetComponentInChildren<Weapon>().Shoot(this);
                isAttack = false;
                timeVal = 0;
            }
        }
        else
        {
            timeVal += Time.deltaTime;
            AwmAttackCDImage.fillAmount = timeVal / attackSpeed;
        }
            
    }

    public void IsAwmAttack()
    {
        if (timeVal < attackSpeed)
            return;
        isAttack = true;
    }

    public void TransformMod()
    {
        if (isTransform)
        {
            isTransform = false;
            mainCamera.orthographicSize /= 2;
            awmMod.SetActive(false);
            normalMod.SetActive(true);
            weaponTrans.Find("Second").gameObject.SetActive(true);
            weaponTrans.Find("First").gameObject.SetActive(false);
            firePoint = weaponTrans.Find("Second").Find("FirePoint").transform;
            if (isPenetrate)
            {
                isPenetrate = false;
            }
            bulletPrefab = tunning.instance.playerGun_bulletPrefab;
            bulletForce = tunning.instance.playerAwm_bulletForce;
            sight = tunning.instance.playerAwm_sight;
            scope = tunning.instance.playerAwm_scope;
        }
        else
        {
            isTransform = true;
            mainCamera.orthographicSize *= 2;
            awmMod.SetActive(true);
            normalMod.SetActive(false);
            weaponTrans.Find("Second").gameObject.SetActive(false);
            weaponTrans.Find("First").gameObject.SetActive(true);
            firePoint = weaponTrans.Find("First").Find("FirePoint").transform;
            bulletPrefab = tunning.instance.playerAwm_bulletPrefab;
            bulletForce = tunning.instance.playerAwm_bulletForceAwm;
            sight = tunning.instance.playerAwm_sightAwm;
            scope = tunning.instance.playerAwm_scopeAwm;
            movement = Vector2.zero;
        }
    }
   

    public void RedDot()
    {
        if (isRedDot)
        {
            isRedDot = false;
            redDot.gameObject.SetActive(false);
        }
        else
        {
            isRedDot = true;
            redDot.gameObject.SetActive(true);
        }
    }

    public void Penetrate()
    {
        if (CD1 > 0)
            return;
        isPenetrate = true;
    }

    
}
