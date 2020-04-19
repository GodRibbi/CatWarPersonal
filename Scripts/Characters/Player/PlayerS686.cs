using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerS686 : Remote
{
    public static PlayerS686 instance;
    //攻击方向
    public Vector2 target;
    //移动方向
    public Vector2 movement;
    public Vector2 dashTarget;
    
    [Header("冲刺属性")]
    public float dashTime = 0.2f;
    public bool isDashing = false;
    [Header("手雷属性")]
    public GameObject grenade;

    [Header("自动装填属性")]
    public const int reloadNumMax = 10;
    public int reloadNum;
    public Text reloadNumText;

    public Image dashCDImage;
    public Image grenadeCDImage;
    public Image autoReloadCDImage;

    public bool isAttack;

    private float timeVal;

    public float CD1;
    public float CD2;
    public float CD3;

    private float dashCD = 3f;
    private float dashSpeed;
    private float dashtime = 0;

    private float grenadeCD = 3f;


    private Vector2 nowLook = Vector2.zero;

    private MaterialTintColor gun;
    private SpriteRenderer tail;
    private SkillRangeControl skillRange;
    private Transform bandolier;
    private void Awake()
    {
        instance = this;
        SetCharacter(Character.Player);


        isAttack = false;


        moveSpeed = tunning.instance.playerS686_moveSpeed;
        attackSpeed = tunning.instance.playerS686_attackSpeed;
        Atk = tunning.instance.playerS686_Atk;
        defence = tunning.instance.playerS686_defence;
        magicPower = tunning.instance.playerS686_magicPower;
        magicDefence = tunning.instance.playerS686_magicDefence;
        SetMaxHP(tunning.instance.playerS686_maxHP);
        SetMaxMP(tunning.instance.playerS686_maxMP);
        bulletForce = tunning.instance.playerS686_bulletForce;
        sight = tunning.instance.playerS686_sight;
        scope = tunning.instance.playerS686_scope;

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
        firePoint = transform.Find("Weapon").Find("FirePoint").transform;
        bulletPrefab = tunning.instance.playerS686_bulletPrefab;
        //bulletPrefab = go;
        characterRb = GetComponent<Rigidbody2D>();
        characterAnima = GetComponent<Animator>();
        body = transform.Find("body").GetComponent<SpriteRenderer>();
        tail = transform.Find("tail").GetComponent<SpriteRenderer>();
        weaponTrans = transform.Find("Weapon").GetComponent<Transform>();
        leftHand = transform.Find("left_hand").GetComponent<Transform>();
        rightHand = transform.Find("right_hand").GetComponent<Transform>();
        timeVal = 0;

        skillRange = transform.Find("SkillRange").GetComponent<SkillRangeControl>();
        bandolier = GameObject.FindGameObjectWithTag("bandolier").transform;
        reloadNum = 5;


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
        AutoReload();
        Dash();
        if (isDashing)
            return;
        Attack();
        Movement();
        
    }
    protected override void Movement()
    {
        SetLookAt();
        //movement.x = Input.GetAxis("Horizontal");
        //movement.y = Input.GetAxis("Vertical");
        //Debug.Log(movement);
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
            //weaponTrans.Find("FirePoint").localPosition = new Vector3(0.28f, -0.07f, 0);
            weaponTrans.GetComponentInChildren<SpriteRenderer>().flipY = true;
            weaponTrans.position = rightHand.position;
        }
        else if (target.x > 0)
        {

            body.flipX = false;
            tail.flipX = false;

           // weaponTrans.Find("FirePoint").localPosition = new Vector3(0.28f, 0.08f, 0);
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
        if (reloadNum <= 0)
            return;
        if (timeVal >= (attackSpeed/2))
        {
            if (isAttack)
            {
                reloadNum--;
                weaponTrans.GetComponentInChildren<Weapon>().ShotGunShoot(this);
            }
            timeVal = 0;
        }
        else
            timeVal += Time.deltaTime;
    }
    public void DashButton()
    {
        if (CD1 != 0)
            return;
        if (reloadNum <= reloadNumMax - 2)
            reloadNum += 2;
        else
            reloadNum = reloadNumMax;
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
    public void Grenade()
    {
        if (CD2 != 0)
            return;
        if (reloadNum <= reloadNumMax - 2)
            reloadNum += 2;
        else
            reloadNum = reloadNumMax;
        GameObject grenades= ObjectPool.GetInstance().GetObj(grenade,
            transform.position,
            bandolier);
        grenades.transform.GetComponent<Grenade>().targer = skillRange.GetPos();
        CD2 = grenadeCD;
    }
    public void AutoReload()
    {
        reloadNumText.text = reloadNum.ToString();
        if (reloadNum >= reloadNumMax)
        {
            reloadNum = reloadNumMax;
            return;
        }
        if (CD3 <= 0)
        {
            CD3 = attackSpeed;
            reloadNum++;
            autoReloadCDImage.fillAmount = 0;
        }
        else
        {
            CD3 -= Time.deltaTime;
            autoReloadCDImage.fillAmount = (attackSpeed - CD3) / attackSpeed;
        }
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
            grenadeCDImage.fillAmount = 1;
        }
        else
        {
            CD2 -= Time.deltaTime;
            grenadeCDImage.fillAmount = (grenadeCD - CD2) / grenadeCD;
        }
    }
    protected override void Stop()
    {
        base.Stop();
    }

}
