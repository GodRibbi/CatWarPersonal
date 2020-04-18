using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunAttackControl : UIControl 
{
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        PlayerGun.instance.target = handlerPosition;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        PlayerGun.instance.isAttack = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        PlayerGun.instance.isAttack = false;
        base.OnPointerUp(eventData);
    }
}
