using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO decide if this can be taken out as an Interface
// Potentially abstract out the trigger?

public class DoorController : MonoBehaviour
{
    public float openSpeed = 0.01f;
    public int doorRotationRange = 90;
    // doorForwardDirection as default forward is along Z
    // need to find a better way to figure this out
    public Vector3 doorForwardDirection = new Vector3(1, 0, 0);

    private float defaultRotation;
    private bool doorOpening = false;
    private bool doorOpeningInward;

    void Start()
    {
        defaultRotation = transform.eulerAngles.y;
    }


    void FixedUpdate()
    {
        if (doorOpening)
        {
            openDoor();
        }
        else if (isDoorOpen())
        {
            resetDoorAngle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !doorOpening)
        {
            Transform playerTransform = other.transform;
            Transform doorTransform = this.transform;
            doorOpeningInward = !isPlayerInforntOfDoor(playerTransform, doorTransform);
            doorOpening = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        StartCoroutine(doorClosingWaitCoroutine(2));

    }

    IEnumerator doorClosingWaitCoroutine(int waitTime)
    {
        //yield on a new YieldInstruction that waits for x seconds.
        yield return new WaitForSeconds(waitTime);
        doorOpening = false;
    }

    public bool isPlayerInforntOfDoor(Transform playerTransform, Transform doorTransform)
    {
        // Gets a heading vector from player to door
        var heading = playerTransform.position - doorTransform.position;
        // dot product of heading with regards to door forward
        // directly correlates to the cosine of angle between door forward and player
        //      dot > 0 if in front
        //      dot < 0 if behind
        //      -1 <= dot <= 1
        // float dotProduct = Vector3.Dot(heading, doorTransform.forward);
        float dotProduct = Vector3.Dot(heading, doorForwardDirection);
        Debug.Log(dotProduct);

        if (dotProduct >= 0f)
        {
            return true;
        }
        return false;
    }

    public bool isDoorOpen()
    {
        if (getCurrentRotation() == defaultRotation)
        {
            return false;
        }
        return true;
    }

    private float getCurrentRotation()
    {
        return transform.eulerAngles.y;
    }

    void resetDoorAngle()
    {
        rotateDoortoAngle((int)defaultRotation, 0.001f);
    }

    void openDoor()
    {
        if (doorOpeningInward)
        {
            openDoorInward();
        }
        else
        {
            openDoorOutward();
        }
    }

    void openDoorOutward()
    {
        rotateDoortoAngle(-doorRotationRange);
    }

    void openDoorInward()
    {
        rotateDoortoAngle(doorRotationRange);
    }

    void rotateDoortoAngle(int angle)
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation, Quaternion.Euler(0, angle, 0), openSpeed * Time.time
            );
    }
    void rotateDoortoAngle(int angle, float openSpeed)
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation, Quaternion.Euler(0, angle, 0), openSpeed * Time.time
            );
    }
}
