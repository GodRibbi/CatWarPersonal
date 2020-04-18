using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S686MovementControl : UIControl
{
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        PlayerS686.instance.movement = handlerPosition;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        handlerPosition = Vector2.zero;
        PlayerS686.instance.movement = handlerPosition;
    }
}
