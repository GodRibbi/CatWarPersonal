using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S686DashControl : UIControl
{

    public override void OnDrag(PointerEventData eventData)
    {
        if (PlayerS686.instance.CD1 != 0)
            return;
        base.OnDrag(eventData);
        PlayerS686.instance.dashTarget = handlerPosition;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerS686.instance.CD1 != 0)
            return;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerS686.instance.CD1 != 0)
            return;
        transform.GetChild(0).gameObject.SetActive(false);
        PlayerS686.instance.DashButton();
        base.OnPointerUp(eventData);
    }
}
