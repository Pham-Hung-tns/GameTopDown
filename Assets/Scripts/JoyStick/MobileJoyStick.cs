using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joyStickTransform;

    [SerializeField]
    private float dragThreshold = 0.6f;
    [SerializeField]
    private int dragMovementDistance = 1;
    [SerializeField]
    private int dragOffsetDistance = 1;

    public event Action<Vector2> OnMove;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joyStickTransform,
            eventData.position,
            null,
            out offset);
        offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;

        joyStickTransform.anchoredPosition = offset * dragMovementDistance;

        Vector2 inputVector = CaculateMovement(offset);
        OnMove?.Invoke(inputVector);
    }

    private Vector2 CaculateMovement(Vector2 offset)
    {
        float x = Mathf.Abs(offset.x) > dragThreshold ? offset.x : 0;
        float y = Mathf.Abs(offset.y) > dragThreshold ? offset.y : 0;   
        return new Vector2(x, y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("quay lai");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joyStickTransform.anchoredPosition = Vector2.zero;
        OnMove?.Invoke(Vector2.zero);
    }

    private void Awake()
    {
        joyStickTransform = (RectTransform)transform;
    }
}
