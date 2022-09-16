using UnityEngine;
/// <summary>
/// This class add the basic functionallity for input controlling a character controller
/// </summary>
public class BaseInputController : MonoBehaviour
{
    public DragDirection Direction { get { return dragDirection; } }
    protected DragDirection dragDirection;
}

//The input direction in which character will move
public enum DragDirection { None = 0, Left = -1, Right = 1 }