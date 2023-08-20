using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [HideInInspector] public Vector2 movementDirection;
    [HideInInspector] public float jump = 0;
    [HideInInspector] public float sprint = 0;
    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementDirection = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        jump = ctx.ReadValue<float>();
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        sprint = ctx.ReadValue<float>();
    }

}
