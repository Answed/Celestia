using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform orientation;
    public Transform combatLookAt;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public InputHandler inputHandler;

    public float rotationSpeed;

    private CinemachineFreeLook cam;

    private void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
    }



    private void Update()
    {
       /* Vector3 viewDir = player.position -  new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
        Vector3 inputDir = new Vector3(inputHandler.movementDirection.x, 0, inputHandler.movementDirection.y);

        if(inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized,Time.deltaTime * rotationSpeed); */

        Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
        orientation.forward = dirToCombatLookAt.normalized;

        playerObj.forward = dirToCombatLookAt.normalized;

        if (inputHandler.zoom > 0 && cam.m_Lens.FieldOfView > 30)
            cam.m_Lens.FieldOfView -= 1;
        else if(inputHandler.zoom < 0 && cam.m_Lens.FieldOfView < 70)
            cam.m_Lens.FieldOfView += 1;
    }
}
