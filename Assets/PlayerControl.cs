using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int speed = 10;
    public Rigidbody rb;

    public Transform cameraTransform;

    public float lookSensitivity = 5f;
    public float leftRight;
    public float upDown;
    public float currentYRotation;
    public float currentXRotation;
    public float yRotationV;
    public float xRotationV;
    public float lookSmoothDamp = 0.06f;
    public float lookClamp;

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * 0.1f, ForceMode.VelocityChange);

        leftRight += Input.GetAxis("Mouse X") * lookSensitivity;
        upDown -= Input.GetAxis("Mouse Y") * lookSensitivity;

        upDown = Mathf.Clamp(upDown, -lookClamp, lookClamp);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, upDown, ref xRotationV, lookSmoothDamp);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, leftRight, ref yRotationV, lookSmoothDamp);

        cameraTransform.localRotation = Quaternion.Euler(currentXRotation, 0, 0);
        rb.MoveRotation(Quaternion.Euler(0, currentYRotation, 0));
    }
}
