using System.Collections;
using UnityEngine;

public class ScaryMovement : MonoBehaviour
{
    public float moveDistance = 1.5f; // Distance to move vertically
    public float moveSpeed = 2f; // Speed of the vertical movement

    public float rotationAngle = 10f; // Angle of rotation on z-axis
    public float rotationSpeed = 2f; // Speed of the rotation

    public float scaleChange = 0.5f; // Amount to scale down
    public float scaleSpeed = 2f; // Speed of the scaling

    private bool goingUp = true; // Controls the vertical movement direction
    private Vector3 initialPosition; // Initial position of the object
    private Vector3 initialScale; // Initial scale of the object

    private void Start()
    {
        initialPosition = transform.position; // Store the initial position
        initialScale = transform.localScale; // Store the initial scale
    }

    private void Update()
    {
        // Vertical movement
        float moveOffset = moveDistance * Time.deltaTime * moveSpeed;
        if (goingUp)
        {
            transform.Translate(Vector3.up * moveOffset);
        }
        else
        {
            transform.Translate(Vector3.down * moveOffset);
        }

        // Rotation
        float rotationOffset = rotationAngle * Time.deltaTime * rotationSpeed;
        if (goingUp)
        {
            transform.Rotate(Vector3.forward * rotationOffset);
        }
        else
        {
            transform.Rotate(Vector3.back * rotationOffset);
        }

        // Scale down and return to normal
        float scaleOffset = scaleChange * Time.deltaTime * scaleSpeed;
        if (goingUp)
        {
            // Scale down
            transform.localScale -= new Vector3(scaleOffset, scaleOffset, scaleOffset);
        }
        else
        {
            // Return to normal scale
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, scaleOffset);
        }

        // Change movement direction when reaching the top or bottom
        if (transform.position.y >= initialPosition.y + moveDistance)
        {
            goingUp = false;
        }
        else if (transform.position.y <= initialPosition.y - moveDistance)
        {
            goingUp = true;
        }
    }
}