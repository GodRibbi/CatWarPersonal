using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S686GrenadeControl : UIControl
{
    public GameObject skillRange;
    public override void OnDrag(PointerEventData eventData)
    {
        if (PlayerS686.instance.CD2 != 0)
            return;
        base.OnDrag(eventData);
        skillRange.GetComponent<SkillRangeControl>().target = handlerPosition;
        PlayerS686.instance.target = handlerPosition;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerS686.instance.CD2 != 0)
            return;
        skillRange.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerS686.instance.CD2 != 0)
            return;
        transform.GetChild(0).gameObject.SetActive(false);
        skillRange.SetActive(false);
        PlayerS686.instance.Grenade();
        base.OnPointerUp(eventData);
    }
}
