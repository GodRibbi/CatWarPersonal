using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangerAttackControl : UIControl
{
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        PlayerRanger.instance.target = handlerPosition;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        PlayerRanger.instance.ArrowTransfrom();
        PlayerRanger.instance.isAttack = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        PlayerRanger.instance.isAttack = false;
        PlayerRanger.instance.Attack();
        base.OnPointerUp(eventData);
    }
}
