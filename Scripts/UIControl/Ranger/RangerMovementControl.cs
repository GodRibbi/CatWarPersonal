using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangerMovementControl : UIControl
{

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        PlayerRanger.instance.movement = handlerPosition;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        handlerPosition = Vector2.zero;
        PlayerRanger.instance.movement = handlerPosition;
    }
}
