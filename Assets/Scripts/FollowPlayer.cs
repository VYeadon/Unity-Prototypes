using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 playerTransformOffset;

    public float speedH = 2.0f;
    public float speedV = 2.0f;


    private float yaw = 0.0f;
    private float playerYaw = 0.0f;
    private float pitch = 0.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = playerTransform.position; 
        
        transform.position = playerPosition + playerTransformOffset;
        
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        playerYaw += speedH * Input.GetAxis("Horizontal");

        transform.RotateAround(playerPosition, new Vector3(0f, playerYaw, 0f), 50 * Time.deltaTime);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    public static Vector3 getRelativePosition(Transform origin, Vector3 position)
    {
        Vector3 distance = position - origin.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
        relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
        relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);

        return relativePosition;
    }
}
