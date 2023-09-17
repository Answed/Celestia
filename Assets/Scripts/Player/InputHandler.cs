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
    [HideInInspector] public bool basicAttackReleased;
    [HideInInspector] public float spell1 = 0;
    [HideInInspector] public bool spell1Released;
    [HideInInspector] public float spell2 = 0;
    [HideInInspector] public bool spell2Released;
    [HideInInspector] public float ultimate = 0;
    [HideInInspector] public bool ultimateReleased;
    [HideInInspector] public float pauseGame;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
    }

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
        basicAttackReleased = ctx.canceled;
    }

    public void OnSpell1(InputAction.CallbackContext ctx)
    {
        spell1 = ctx.ReadValue<float>();
        spell1Released = ctx.canceled;
    }

    public void OnSpell2(InputAction.CallbackContext ctx)
    {
        spell2 = ctx.ReadValue<float>();
        spell2Released = ctx.canceled;
    }

    public void OnUltimate(InputAction.CallbackContext ctx)
    {
        ultimate  = ctx.ReadValue<float>();
        ultimateReleased = ctx.canceled;
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        gameManager.PauseGame();
    }
}
