using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;
    [SerializeField] private float sprintMultiplikator;
    [SerializeField] private float dashDelay;

    private int jumpcounter;
    private bool onGround;
    private float nextDash;

    private Rigidbody rb;
    private InputHandler inputHandler;
    private Transform playerObject;
    private Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        playerObject = GameObject.Find("PlayerObject").GetComponent<Transform>();
    }

    private void Update()
    {
        moveDir = inputHandler.movementDirection;
    }

    private void FixedUpdate()
    {
        if ((onGround || jumpcounter <= 1) && inputHandler.jump == 1)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpcounter++;
            inputHandler.jump = 0;
        }

        if(inputHandler.dash == 1 && nextDash <= Time.time)
        {
            inputHandler.dash = 0;
            transform.Translate(dashForce * Time.deltaTime * (playerObject.forward * moveDir.y + playerObject.right * moveDir.x));
            nextDash = Time.time + dashDelay;
        }

        if (inputHandler.sprint == 1)
           transform.position += movementSpeed * Time.deltaTime * sprintMultiplikator *  (playerObject.forward * moveDir.y + playerObject.right * moveDir.x);
        else transform.position += movementSpeed * Time.deltaTime * (playerObject.forward * moveDir.y + playerObject.right * moveDir.x);
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
