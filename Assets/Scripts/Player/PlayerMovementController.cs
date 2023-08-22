using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;
    [SerializeField] private float sprintMultiplyer;
    [SerializeField] private float crouchMultiplyer;
    [SerializeField] private float dashDelay;

    private int jumpcounter;
    private bool onGround;
    private float nextDash;
    private bool isDashing;

    private Rigidbody rb;
    private InputHandler inputHandler;
    private Transform playerObject;
    private Vector2 moveDir;
    private Vector3 vel;

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
            isDashing = true;
            inputHandler.dash = 0;
            rb.AddForce(vel * dashForce, ForceMode.Impulse);    
            nextDash = Time.time + dashDelay;
            StartCoroutine(isDahsing());
        }

        if (inputHandler.sprint == 1)
           vel = movementSpeed * 10 * Time.deltaTime * sprintMultiplyer *  (playerObject.forward * moveDir.y + playerObject.right * moveDir.x);
        else if (inputHandler.crouch == 1)
            vel = movementSpeed * 10 * Time.deltaTime * crouchMultiplyer * (playerObject.forward * moveDir.y + playerObject.right * moveDir.x);
        else vel = movementSpeed * 10 * Time.deltaTime * (playerObject.forward * moveDir.y + playerObject.right * moveDir.x);

        if(!isDashing)
        {
            vel.y = rb.velocity.y;
            rb.velocity = vel;
        }
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

    IEnumerator isDahsing()
    {
        yield return new WaitForSeconds(0.3f);
        isDashing = false;

    }
}
