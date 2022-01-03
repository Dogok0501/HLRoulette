using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSightController : MonoBehaviour
{
    [SerializeField] private float sensX = 100;
    [SerializeField] private float sensY = 100;

    Camera cam;

    [SerializeField] Joystick joystick;

    private float mouseX;
    private float mouseY;

    private float multiplier = 0.01f;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    private void Update()
    {
        MyInput();
    }

    private void LateUpdate()
    {
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void MyInput()
    {
        mouseX = joystick.Horizontal;
        mouseY = joystick.Vertical;

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
    }
}
