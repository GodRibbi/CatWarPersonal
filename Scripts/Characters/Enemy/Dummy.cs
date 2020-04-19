using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : Remote
{
    public float AtkD;
    public float defenceD;
    public float magicDefenceD;
    public float maxHPD;
    [SerializeField]
    private float HPD;

    private void Awake()
    {
        SetCharacter(Character.Enemy);



        Atk = AtkD;
        defence = defenceD;
        magicDefence = magicDefenceD;
        SetMaxHP(maxHPD);


        materialTintColor = GetComponent<MaterialTintColor>();
        materialTintColor.SetMaterial(transform.Find("body").GetComponent<SpriteRenderer>().material);
        characterRb = GetComponent<Rigidbody2D>();
        HpFill = transform.Find("UI").Find("bloodbar").GetComponent<Image>();
        body = transform.Find("body").GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        HPD = GetHP();
        if (stop)
            Stop();
    }
    protected override void Stop()
    {
        base.Stop();
    }
}
