using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sprintMultiplikator;

    private int jumpcounter;
    private bool onGround;

    private Rigidbody rb;
    private InputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
    }

    private void FixedUpdate()
    {
        if(inputHandler.sprint == 1)
            transform.position += new Vector3(inputHandler.movementDirection.x, 0, inputHandler.movementDirection.y) * movementSpeed * sprintMultiplikator * Time.deltaTime;
        else transform.position += new Vector3(inputHandler.movementDirection.x, 0, inputHandler.movementDirection.y) * movementSpeed * Time.deltaTime;

        if ((onGround || jumpcounter <= 1) && inputHandler.jump == 1)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpcounter++;
            inputHandler.jump = 0;
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
}
