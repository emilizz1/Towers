using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Pip : MonoBehaviour, IDragHandler, IPointerDownHandler {

    private Vector2 pointerOffset;
    private RectTransform canvasRectTransform;
    private RectTransform pipRectTransform;
    private RectTransform fieldRectTransform;

    public bool pipIsMovable = false;

    Canvas canvas;
    Field field;

    void Awake ()
    {
        canvas = GetComponentInParent<Canvas>();

        ChangedParentField();

        if (canvas != null)
        {
            canvasRectTransform = canvas.transform as RectTransform;
            pipRectTransform = transform as RectTransform;
        }
    }

    public void ChangedParentField()
    {
        field = GetComponentInParent<Field>();
        if (canvas != null)
        {
            fieldRectTransform = field.transform as RectTransform;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        pipRectTransform.SetAsLastSibling();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(pipRectTransform, data.position, data.pressEventCamera, out pointerOffset);
        
    }

    public void OnDrag(PointerEventData data)
    {
        if(pipRectTransform == null)
        {
            return;
        }

        Vector2 pointerPosition = ClampToWindow(data);
        if (pipIsMovable)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(fieldRectTransform, pointerPosition, data.pressEventCamera, out localPointerPosition))
            {
                pipRectTransform.localPosition = localPointerPosition - pointerOffset;
            }
        }
    }

    Vector2 ClampToWindow(PointerEventData data)
    {
        Vector2 rawPointerPosition = data.position;

        Vector3[] canvasCorners = new Vector3[4];
        canvasRectTransform.GetWorldCorners(canvasCorners);

        float clampX = Mathf.Clamp (rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        float clampY = Mathf.Clamp (rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);


        Vector2 newPointerPosition = new Vector2(clampX, clampY);
        return newPointerPosition;
    }
}
