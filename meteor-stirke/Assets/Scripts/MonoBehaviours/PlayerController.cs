using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mouseSensitivity = 5;          // How sensitive the camera movement will be.
    public float lookSmoothDamp = 0.1f;         // The time it will take for the camera to reach the desired mouse position.

    private float xRotation;                    // The camera's rotation around the X axis - input from the mouse's Y axis.
    private float yRotation;                    // The camera's rotation around the Y axis - input from the mouse's X axis.
    private float currentXRotation;             // The camera's current rotation around the X axis this frame.
    private float currentYRotation;             // The camera's current rotation around the Y axis this frame.
    private float xRotationVelocity = 0.0f;     // The current rotational velocity of the camera around the X axis.
    private float yRotationVelocity = 0.0f;     // The current rotational velocity of the camera around the Y axis.

    /* Called once every frame. */
    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked/*lockedCursor*/)
        {
            // Get the input axis values from the mouse.
            xRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            yRotation += Input.GetAxis("Mouse X") * mouseSensitivity;

            // Clamps rotation around the X axis to avoid flipping the camera.
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            // Smoothly update the camera's current rotational values towards the desired value each frame.
            currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVelocity, lookSmoothDamp);
            currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationVelocity, lookSmoothDamp);

            // Apply the rotation.
            transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
        }
    }

    /* Sets the mouse senstivity. */
    public void SetMouseSensitivity(float sensitivty)
    {
        mouseSensitivity = sensitivty;
    }
}