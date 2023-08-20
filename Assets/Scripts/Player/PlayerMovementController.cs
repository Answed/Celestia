using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private Vector2 movementDirection;
    private float jump = 0;
    private int jumpcounter;
    private bool onGround;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movementDirection.x, 0, movementDirection.y) * movementSpeed * Time.deltaTime;

        if ((onGround || jumpcounter <= 1) && jump == 1)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpcounter++;
            jump = 0;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementDirection = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        jump = ctx.ReadValue<float>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            jumpcounter = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            onGround = false;
    }
}
