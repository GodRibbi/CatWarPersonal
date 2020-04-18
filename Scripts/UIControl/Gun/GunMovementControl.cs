using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunMovementControl : UIControl
{

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        PlayerGun.instance.movement = handlerPosition;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        handlerPosition = Vector2.zero;
        PlayerGun.instance.movement = handlerPosition;
        
    }
}
