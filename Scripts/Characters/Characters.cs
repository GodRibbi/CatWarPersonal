using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Character
{
    Player = 1,
    Enemy = 2,
    Npc = 3
}


public class Characters : MonoBehaviour
{
    private Character character;
    private float stopTime = 0;

    protected Transform weaponTrans;
    protected Animator characterAnima;
    protected Rigidbody2D characterRb;
    protected Image HpFill;
    protected Image MpFill;
    protected MaterialTintColor materialTintColor;


    protected float moveSpeed;
    protected float attackSpeed;
    protected float Atk;
    protected float defence;
    protected float magicPower;
    protected float magicDefence;
    protected float magicReuse;


    private float healthPoint;
    private float magicPoint;
    private float maxHP;
    private float maxMP;

    protected bool dead = false;
    protected bool stop = false;

    protected virtual void Dead() 
    {
        Invoke("GoDead", 0.1f);
    }
    private void GoDead()
    {
        ObjectPool.GetInstance().RecycleObj(gameObject);
    }
    protected virtual void Attack() { }

    protected virtual void Movement() { }

    protected virtual void Anim() { }
    protected virtual void Stop()
    {
        if (stopTime <= 0)
        {
            stop = false;
            stopTime = 0;
        }
        else
        {
            stop = true;
            stopTime -= Time.fixedDeltaTime;
        }
    }
    public void PhysicalDamage(float damage, float stopTime = 0.03f)
    {
        HealthDelay(-(damage / (1 + defence / 100)));
        this.stopTime += stopTime;
        Stop();
    }

    public void MagicDamage(float damage)
    {
        HealthDelay(-(damage / (1 + magicDefence / 100)));
    }

    public void HealthDelay(float delay)
    {
        if (delay > 0)
            materialTintColor.SetTintGreen();
        else if (delay < 0)
            materialTintColor.SetTintRed();
        healthPoint += delay;
        if (healthPoint <= 0)
        {
            dead = true;
            Dead();
            return;
        }
        else if (healthPoint >= maxHP)
        {
            healthPoint = maxHP;
            return;
        }
        HpFill.fillAmount = GetHealthPercent();
    }

    public void MagicDelay(float delay)
    {
        if (character == Character.Enemy)
        {
            Debug.LogError("Enemy don't have MP");
            return;
        }
           
        magicPoint += delay;
        if (magicPoint <= 0)
        {
            magicPoint = 0;
            return;
        }
        else if (magicPoint >= maxMP)
        {
            magicPoint = maxMP;
            return;
        }
        MpFill.fillAmount = GetMagicPoint();
    }

    public float GetHealthPercent()
    {
        if (healthPoint == maxHP)
            return 1;
        else
            return healthPoint / maxHP;
    }

    public float GetMagicPoint()
    {
        if (character == Character.Enemy)
        {
            Debug.LogError("Enemy don't have MP");
            return -1f;
        }
        if (magicPoint == maxMP)
            return 1;
        else
            return magicPoint / maxMP;
    }

    public void MoveSpeedDelay(float delay)
    {
        moveSpeed += delay;
    }
    public void AttackSpeedDelay(float delay)
    {
        attackSpeed += delay;
    }
    public void AtkDelay(float delay)
    {
        Atk += delay;
    }
    public void DefenceDelay(float delay)
    {
        defence += delay;
    }
    public void MagicPowerDelay(float delay)
    {
        magicPower += delay;
    }
    public void MagicDefenceDelay(float delay)
    {
        magicDefence += delay;
    }

    protected void SetCharacter(Character character)
    {
        this.character = character;
    }
    public void SetMoveSpeed(float num)
    {
        moveSpeed = num;
    }
    public void SetAttackSpeed(float num)
    {
        attackSpeed = num;
    }
    public void SetAtk(float num)
    {
        Atk = num;
    }
    public void SetHP(float num)
    {
        if (healthPoint <= maxHP)
            healthPoint = num;
        else
            Debug.LogError("SetHPError");
    }
    public void SetMP(float num)
    {
        if (character == Character.Enemy)
        {
            Debug.LogError("Enemy don't have MP");
            return;
        }
        if (magicPoint <= maxMP)
            magicPoint = num;
        else
            Debug.LogError("SetMPError");
    }
    public void SetMaxHP(float num)
    {
        healthPoint = maxHP = num;
    }
    public void SetMaxMP(float num)
    {
        if (character == Character.Enemy)
        {
            Debug.LogError("Enemy don't have MP");
            return;
        }
        magicPoint = maxMP = num;
    }
    public void SetDefence(float num)
    {
        defence = num;
    }
    public void SetMagicPower(float num)
    {
        magicPower = num;
    }
    public void SetMagicDefence(float num)
    {
        magicDefence = num;
    }

    public bool GetCharacter(Character character)
    {
        if (this.character == character)
            return true;
        else
            return false;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
    public float GetAtk()
    {
        return Atk;
    }
    public float GetHP()
    {
        return healthPoint;
    }
    public float GetMP()
    {
        if (character == Character.Enemy)
            return 0;
        return magicPoint;
    }
    public float GetMaxHP()
    {
        return maxHP;
    }
    public float GetMaxMP()
    {
        if (character == Character.Enemy)
        {
            Debug.LogError("Enemy don't have MP");
            return -1;
        }
        return maxMP;
    }
    public float GetDefence()
    {
        return defence;
    }
    public float GetMagicPower()
    {
        if (character == Character.Enemy)
            return 0;
        return magicPower;
    }
    public float GetMagicDefence()
    {
        return magicDefence;
    }
    public bool GetIsDead()
    {
        return dead;
    }
    public void SetDead(bool isDead)
    {
        dead = isDead;
    }
    public void Resurgence()
    {
        SetDead(false);
        healthPoint = maxHP;
        HpFill.fillAmount = 1;
    }
    public Rigidbody2D GetRb()
    {
        return characterRb;
    }
}
