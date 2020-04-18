using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S686AttackControl : UIControl
{
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        PlayerS686.instance.target = handlerPosition;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        PlayerS686.instance.isAttack = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        PlayerS686.instance.isAttack = false;
        base.OnPointerUp(eventData);
    }
}
