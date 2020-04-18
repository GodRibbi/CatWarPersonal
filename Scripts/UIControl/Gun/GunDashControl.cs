using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunDashControl : UIControl
{

    public override void OnDrag(PointerEventData eventData)
    {
        if (PlayerGun.instance.CD1 != 0)
            return;
        base.OnDrag(eventData);
        PlayerGun.instance.dashTarget = handlerPosition;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerGun.instance.CD1 != 0)
            return;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerGun.instance.CD1 != 0)
            return;
        transform.GetChild(0).gameObject.SetActive(false);
        PlayerGun.instance.DashButton();
        base.OnPointerUp(eventData);
    }
}
