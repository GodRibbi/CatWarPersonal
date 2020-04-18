using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRanger : Remote
{
    public static PlayerRanger instance;
    //攻击方向
    public Vector2 target;
    //移动方向
    public Vector2 movement;
    public GameObject arrows;
    public Camera mainCamera;

    private GameObject arrow;


    [Header("冲刺属性")]
    public float dashTime = 0.2f;
    public bool isDashing = false;
    [Header("手雷属性")]
    public GameObject grenade;

    [Header("自动装填属性")]
    public int reloadNum;
    public Text reloadNumText;
    public const int reloadNumMax = 10;
    

    public Transform storageBar;
    public Image dashCDImage;
    public Image grenadeCDImage;
    public Image autoReloadCDImage;

    public bool isAttack;
    public float timeVal;
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


        moveSpeed = tunning.instance.playerGun_moveSpeed;
        attackSpeed = tunning.instance.playerGun_attackSpeed * 10;
        Atk = tunning.instance.playerGun_Atk;
        defence = tunning.instance.playerGun_defence;
        magicPower = tunning.instance.playerGun_magicPower;
        magicDefence = tunning.instance.playerGun_magicDefence;
        SetMaxHP(tunning.instance.playerGun_maxHP);
        SetMaxMP(tunning.instance.playerGun_maxMP);
        bulletForce = tunning.instance.playerGun_bulletForce;
        sight = tunning.instance.playerGun_sight/2;
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


        //gun = transform.Find("Weapon").Find("Gun").GetComponent<MaterialTintColor>();
        //gun.SetMaterial(transform.Find("Weapon").Find("Gun").GetComponent<SpriteRenderer>().material);
        materialTintColor = GetComponent<MaterialTintColor>();
        materialTintColor.SetMaterial(transform.Find("body").GetComponent<SpriteRenderer>().material);
        //firePoint = transform.Find("Weapon").Find("FirePoint").transform;
        bulletPrefab = tunning.instance.playerS686_bulletPrefab;
        //bulletPrefab = go;
        characterRb = GetComponent<Rigidbody2D>();
        characterAnima = GetComponent<Animator>();
        body = transform.Find("body").GetComponent<SpriteRenderer>();
        tail = transform.Find("tail").GetComponent<SpriteRenderer>();
        weaponTrans = transform.Find("Weapon").GetComponent<Transform>();
        leftHand = transform.Find("left_hand").GetComponent<Transform>();
        rightHand = transform.Find("right_hand").GetComponent<Transform>();

        bandolier = GameObject.FindGameObjectWithTag("bandolier").transform;
        reloadNum = 5;
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
        if (isDashing)
            return;
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
    public new void Attack()
    {
        if (timeVal < attackSpeed)
        {
            arrow.transform.localRotation =
            Quaternion.AngleAxis(
                Random.Range(-GetSight() * (2 - Percent.GetPercent(timeVal, attackSpeed)), 
                GetSight() * (2 - Percent.GetPercent(timeVal, attackSpeed))),
                Vector3.forward);
        }
        else if (timeVal >= attackSpeed && timeVal < attackSpeed * 2)
        {
            arrow.transform.localRotation =
            Quaternion.AngleAxis(Random.Range(-GetSight() / Percent.GetPercent(timeVal, attackSpeed),
            GetSight() / Percent.GetPercent(timeVal, attackSpeed)), Vector3.forward);
        }
        else if (timeVal >= attackSpeed * 2)
        {
            arrow.transform.localRotation =
            Quaternion.AngleAxis(Random.Range(-GetSight() / 2, GetSight() / 2), Vector3.forward);
        }
        weaponTrans.GetComponentInChildren<Weapon>().Archery(this, timeVal, arrow);
        mainCamera.orthographicSize = 4;
        storageBar.GetChild(0).GetComponent<Image>().fillAmount = 0;
        storageBar.GetChild(1).GetComponent<Image>().fillAmount = 0;
        timeVal = 0;
    }
    public void ArrowTransfrom()
    {
        if (arrow != null)
            arrow = null;
        arrow = ObjectPool.GetInstance().GetObj(arrows, Vector3.zero, weaponTrans);
        arrow.transform.SetParent(weaponTrans);
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
    }
    private void CD()
    {
        if (isAttack)
        {
            if (timeVal >= attackSpeed * 2)
            {
                timeVal = attackSpeed * 2;
            }
            else
                timeVal += Time.deltaTime;
            if (timeVal > 0)
            {
                if (timeVal < attackSpeed)
                {
                    storageBar.GetChild(0).GetComponent<Image>().fillAmount = Percent.GetPercent(timeVal, attackSpeed);
                    arrow.transform.localPosition = new Vector3(-timeVal / 4, 0);
                }
                else if (timeVal >= attackSpeed && timeVal < attackSpeed * 2)
                {
                    storageBar.GetChild(0).GetComponent<Image>().fillAmount = 1;
                    storageBar.GetChild(1).GetComponent<Image>().fillAmount = Percent.GetPercent(timeVal, attackSpeed) - 1;
                    arrow.transform.localPosition = new Vector3(-attackSpeed / 4, 0);
                    mainCamera.orthographicSize = 4 * (Percent.GetPercent(timeVal, attackSpeed) * 0.5f + 0.5f);
                }
                else if (timeVal >= attackSpeed * 2)
                {
                    storageBar.GetChild(0).GetComponent<Image>().fillAmount = 1;
                    storageBar.GetChild(1).GetComponent<Image>().fillAmount = 1;
                    mainCamera.orthographicSize = 6;
                    arrow.transform.localPosition = new Vector3(-attackSpeed / 4, 0);
                }
            }
        }
        //if (CD1 <= 0)
        //{
        //    CD1 = 0;
        //    dashCDImage.fillAmount = 1;
        //}
        //else
        //{
        //    CD1 -= Time.deltaTime;
        //    dashCDImage.fillAmount = (dashCD - CD1) / dashCD;
        //}
        //if (CD2 <= 0)
        //{
        //    CD2 = 0;
        //    grenadeCDImage.fillAmount = 1;
        //}
        //else
        //{
        //    CD2 -= Time.deltaTime;
        //    grenadeCDImage.fillAmount = (grenadeCD - CD2) / grenadeCD;
        //}
    }
    protected override void Stop()
    {
        base.Stop();
    }
}



