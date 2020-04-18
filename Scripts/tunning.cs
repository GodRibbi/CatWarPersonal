using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunning : MonoBehaviour
{
    public static tunning instance;

    [Header("玩家")]
    public Transform player;
    [Header("基地")]
    public Transform home;
    [Space]

    #region Gun
    [Header("PlayerGun")]
    [Tooltip("移动速度")]
    public float playerGun_moveSpeed = 2.5f;
    [Tooltip("攻击速度")]
    public float playerGun_attackSpeed = 0.4f;
    [Tooltip("攻击力")]
    public float playerGun_Atk = 10;
    [Tooltip("防御力")]
    public float playerGun_defence = 5;
    [Tooltip("魔法强度")]
    public float playerGun_magicPower = 0;
    [Tooltip("魔法抗性")]
    public float playerGun_magicDefence = 5;
    [Tooltip("当前生命值")]
    public float playerGun_healthPoint = 0;
    [Tooltip("当前魔法值")]
    public float playerGun_magicPoint = 0;
    [Tooltip("最大生命值")]
    public float playerGun_maxHP = 100;
    [Tooltip("最大魔法值")]
    public float playerGun_maxMP = 100;
    [Tooltip("子弹飞行速度")]
    public float playerGun_bulletForce = 7.5f;
    [Tooltip("武器准星")]
    public int playerGun_sight = 10;
    [Tooltip("射程")]
    public float playerGun_scope = 7.5f;
    [Tooltip("子弹")]
    public GameObject playerGun_bulletPrefab;
    #endregion


    #region Awm
    [Header("PlayerAwm")]
    [Tooltip("移动速度")]
    public float playerAwm_moveSpeed = 2.5f;
    [Tooltip("攻击速度Awm")]
    public float playerAwm_attackSpeedAwm = 1.5f;
    [Tooltip("攻击速度")]
    public float playerAwm_attackSpeed = 0.6f;
    [Tooltip("攻击力")]
    public float playerAwm_Atk = 10;
    [Tooltip("防御力")]
    public float playerAwm_defence = 5;
    [Tooltip("魔法强度")]
    public float playerAwm_magicPower = 0;
    [Tooltip("魔法抗性")]
    public float playerAwm_magicDefence = 5;
    [Tooltip("当前生命值")]
    public float playerAwm_healthPoint = 0;
    [Tooltip("当前魔法值")]
    public float playerAwm_magicPoint = 0;
    [Tooltip("最大生命值")]
    public float playerAwm_maxHP = 100;
    [Tooltip("最大魔法值")]
    public float playerAwm_maxMP = 100;
    [Tooltip("子弹飞行速度Awm")]
    public float playerAwm_bulletForceAwm = 30f;
    [Tooltip("子弹飞行速度")]
    public float playerAwm_bulletForce = 7.5f;
    [Tooltip("武器准星Awm")]
    public int playerAwm_sightAwm = 1;
    [Tooltip("武器准星")]
    public int playerAwm_sight = 10;
    [Tooltip("射程Awm")]
    public float playerAwm_scopeAwm = 20f;
    [Tooltip("射程")]
    public float playerAwm_scope = 7.5f;
    [Tooltip("子弹")]
    public GameObject playerAwm_bulletPrefab;
    #endregion


    #region S686
    [Header("PlayerS686")]
    [Tooltip("移动速度")]
    public float playerS686_moveSpeed = 2.5f;
    [Tooltip("攻击速度")]
    public float playerS686_attackSpeed = 1f;
    [Tooltip("攻击力")]
    public float playerS686_Atk = 10;
    [Tooltip("防御力")]
    public float playerS686_defence = 5;
    [Tooltip("魔法强度")]
    public float playerS686_magicPower = 0;
    [Tooltip("魔法抗性")]
    public float playerS686_magicDefence = 5;
    [Tooltip("当前生命值")]
    public float playerS686_healthPoint = 0;
    [Tooltip("当前魔法值")]
    public float playerS686_magicPoint = 0;
    [Tooltip("最大生命值")]
    public float playerS686_maxHP = 100;
    [Tooltip("最大魔法值")]
    public float playerS686_maxMP = 100;
    [Tooltip("子弹飞行速度")]
    public float playerS686_bulletForce = 25f;
    [Tooltip("武器准星")]
    public int playerS686_sight = 15;
    [Tooltip("射程")]
    public float playerS686_scope = 2f;
    [Tooltip("子弹")]
    public GameObject playerS686_bulletPrefab;
    #endregion

    [Space]
    [Header("EnemyGun")]
    [Tooltip("移动速度")]
    public float enemyGun_moveSpeed;
    [Tooltip("攻击速度")]
    public float enemyGun_attackSpeed;
    [Tooltip("攻击力")]
    public float enemyGun_Atk;
    [Tooltip("防御力")]
    public float enemyGun_defence;
    [Tooltip("魔法抗性")]
    public float enemyGun_magicDefence;
    [Tooltip("当前生命值")]
    public float enemyGun_healthPoint;
    [Tooltip("最大生命值")]
    public float enemyGun_maxHP;
    [Tooltip("子弹飞行速度")]
    public float enemyGun_bulletForce;
    [Tooltip("武器准星")]
    public int enemyGun_sight;
    [Tooltip("射程")]
    public float enemyGun_scope;
    [Tooltip("子弹")]
    public GameObject enemyGun_bulletPrefab;


    [Space]
    [Header("EnemySword")]
    [Tooltip("移动速度")]
    public float enemySword_moveSpeed;
    [Tooltip("攻击速度")]
    public float enemySword_attackSpeed;
    [Tooltip("攻击力")]
    public float enemySword_Atk;
    [Tooltip("防御力")]
    public float enemySword_defence;
    [Tooltip("魔法抗性")]
    public float enemySword_magicDefence;
    [Tooltip("当前生命值")]
    public float enemySword_healthPoint;
    [Tooltip("最大生命值")]
    public float enemySword_maxHP;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

    }
}


public class Percent
{
    /// <summary>
    /// 获得百分比
    /// </summary>
    /// <param name="numerator">分子</param>
    /// <param name="denominator">分母</param>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <returns></returns>
    public static float GetPercent(float numerator, float denominator, float min = 0, float max = 0)
    {
        float percent = -1;
        percent = numerator / denominator;
        if (min != 0)
            if (percent < min)
                percent = min;
        if (max != 0)
            if (percent > max)
                percent = max;
        return percent;
    }
}