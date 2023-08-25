using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [HideInInspector] public Vector2 movementDirection;
    [HideInInspector] public float jump = 0;
    [HideInInspector] public float sprint = 0;
    [HideInInspector] public bool dash = false;
    [HideInInspector] public float crouch = 0;
    [HideInInspector] public float zoom = 0;
    [HideInInspector] public float basicAttack = 0;
    [HideInInspector] public float spell1 = 0;
    [HideInInspector] public float spell2 = 0;
    [HideInInspector] public float ultimate = 0;
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

    public void OnDash(InputAction.CallbackContext ctx)
    {
            dash = ctx.canceled;
    }

    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        crouch = ctx.ReadValue<float>();
    }

    public void OnZoom(InputAction.CallbackContext ctx)
    {
        zoom = ctx.ReadValue<float>();
    }

    public void OnBasicAttack(InputAction.CallbackContext ctx)
    {
        basicAttack = ctx.ReadValue<float>();
    }

    public void OnSpell1(InputAction.CallbackContext ctx)
    {
        spell1 = ctx.ReadValue<float>();
    }

    public void OnSpell2(InputAction.CallbackContext ctx)
    {
        spell2 = ctx.ReadValue<float>();
    }

    public void OnUltimate(InputAction.CallbackContext ctx)
    {
        ultimate  = ctx.ReadValue<float>();
    }
}
