using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("半径")]
    public float radius;
    [Header("拉杆方向")]
    public Vector2 handlerPosition;
    [Header("拉扯")]
    public float handler;

    public Action<Vector2> onJoystickDownEvent;     // 按下事件
    public Action onJoystickUpEvent;     // 抬起事件
    public Action<Vector2> onJoystickMoveEvent;     // 滑动事件

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (Vector2.Distance(eventData.position, transform.position) <= radius)
        {
            transform.GetChild(0).position = eventData.position;
        }
        else
        {
            transform.GetChild(0).position = transform.position +
                Vector3.Normalize((Vector3)eventData.position - transform.position) * radius;
        }
        handlerPosition.y = transform.GetChild(0).position.y / radius - transform.position.y / radius;
        handlerPosition.x = transform.GetChild(0).position.x / radius - transform.position.x / radius;
        handler = Vector2.Distance(transform.GetChild(0).position, transform.position) / radius;
        if (onJoystickMoveEvent != null)
            onJoystickMoveEvent(handlerPosition);
    }


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (onJoystickDownEvent != null)
            onJoystickDownEvent(handlerPosition);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        transform.GetChild(0).localPosition = Vector2.zero;
        handler = 0;
        if (onJoystickUpEvent != null)
            onJoystickUpEvent();
    }
}