using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AwmMovementControl : UIControl
{
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        PlayerAwm.instance.movement = handlerPosition;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        handlerPosition = Vector2.zero;
        PlayerAwm.instance.movement = handlerPosition;

    }
}
