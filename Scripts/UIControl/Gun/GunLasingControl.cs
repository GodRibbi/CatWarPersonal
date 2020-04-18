using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunLasingControl : UIControl
{
    public GameObject go;
    public override void OnDrag(PointerEventData eventData)
    {
        if (PlayerGun.instance.CD2 != 0)
            return;
        base.OnDrag(eventData);
        PlayerGun.instance.target = handlerPosition;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerGun.instance.CD2 != 0)
            return;
        transform.GetChild(0).gameObject.SetActive(true);
        go.SetActive(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerGun.instance.CD2 != 0)
            return;
        transform.GetChild(0).gameObject.SetActive(false);
        go.SetActive(false);
        PlayerGun.instance.LasingButton();
        base.OnPointerUp(eventData);
    }
}
