using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AwmDirectionControl : UIControl
{
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        PlayerAwm.instance.target = handlerPosition;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }
}
