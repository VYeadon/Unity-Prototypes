using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    public Transform camera;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 cameraForwardDirection;
    private Vector3 cameraSidewayDirection;
    private Vector3 forwardMoveDirection;
    private Vector3 sidewayMoveDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // yaw += speedH * Input.GetAxis("Mouse X");
        // pitch -= speedV * Input.GetAxis("Mouse Y");

        // transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            // Applys the movement forces relative to the direction the camera is moving
            cameraForwardDirection = camera.forward;
            cameraSidewayDirection = Rotate90CW(cameraForwardDirection);

            forwardMoveDirection = cameraForwardDirection * Input.GetAxis("Vertical");
            sidewayMoveDirection = cameraSidewayDirection * Input.GetAxis("Horizontal");

            moveDirection = forwardMoveDirection + sidewayMoveDirection;
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    Vector3 Rotate90CW(Vector3 aDir)
    {
        return new Vector3(aDir.z, 0, -aDir.x);
    }
}


