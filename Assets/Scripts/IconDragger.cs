using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class IconDragger : MouseManipulator
{
    Vector2 startPos;
    Vector2 elemStartPosGlobal;
    Vector2 elemStartPosLocal;


    VisualElement dragArea;
    VisualElement iconContainer;
    VisualElement dropZone;

    bool isActive;


    public IconDragger(VisualElement root)
    {
        dragArea = root.Q("DragArea");
        dropZone = root.Q("DropBox");

        isActive = false;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected void OnMouseDown(MouseDownEvent e)
    {
        iconContainer = target.parent;

        startPos = e.localMousePosition;

        //elemStartPosGlobal = new Vector2(target.worldBound.xMin, target.worldBound.yMin);
        elemStartPosGlobal = target.worldBound.position;
        elemStartPosLocal = target.layout.position;


        dragArea.style.display = DisplayStyle.Flex;
        dragArea.Add(target);
        target.style.top = elemStartPosGlobal.y;
        target.style.left = elemStartPosGlobal.x;

        isActive = true;
        target.CaptureMouse();
        e.StopPropagation();
    }
    protected void OnMouseMove(MouseMoveEvent e)
    {
        if(!isActive || !target.HasMouseCapture())
            return;

        Vector2 diff = e.localMousePosition - startPos;

        target.style.top = target.layout.y + diff.y;
        target.style.left = target.layout.x + diff.x;
    }
    protected void OnMouseUp(MouseUpEvent e)
    {
        iconContainer.Add(target);

        target.style.top = elemStartPosLocal.y;
        target.style.left = elemStartPosLocal.x;

        dragArea.style.display = DisplayStyle.None;
        isActive = false;
        target.ReleaseMouse();
        e.StopPropagation();
    }
}
