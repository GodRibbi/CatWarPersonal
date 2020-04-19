using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangerDashControl : UIControl
{

    public override void OnDrag(PointerEventData eventData)
    {
        if (PlayerRanger.instance.CD1 != 0)
            return;
        base.OnDrag(eventData);
        PlayerRanger.instance.dashTarget = handlerPosition;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerRanger.instance.CD1 != 0)
            return;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerRanger.instance.CD1 != 0)
            return;
        transform.GetChild(0).gameObject.SetActive(false);
        PlayerRanger.instance.DashButton();
        base.OnPointerUp(eventData);
    }
}