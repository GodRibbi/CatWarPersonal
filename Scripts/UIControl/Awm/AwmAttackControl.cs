using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AwmAttackControl : UIControl
{
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        PlayerAwm.instance.target = handlerPosition;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        PlayerAwm.instance.isAttack = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        PlayerAwm.instance.isAttack = false;
        base.OnPointerUp(eventData);
    }
}
