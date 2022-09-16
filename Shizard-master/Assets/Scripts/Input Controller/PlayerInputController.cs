using UnityEngine;
/// <summary>
/// This class handle player's input based on screen drag or mouse drag
/// </summary>
public class PlayerInputController : BaseInputController
{
    private Vector3 startMousePosition, currentMousePosition;
    private bool isClicked;

    void Update()
    {
        //Get player drag on screen
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = currentMousePosition = Input.mousePosition;
            isClicked = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startMousePosition = currentMousePosition = Vector2.zero;
            isClicked = false;
        }
        if (isClicked)
            currentMousePosition = Input.mousePosition;

        //Get drag direction
        if (currentMousePosition.x > startMousePosition.x)
            dragDirection = DragDirection.Right;
        else if (currentMousePosition.x < startMousePosition.x)
            dragDirection = DragDirection.Left;
        else
            dragDirection = DragDirection.None;
    }
}