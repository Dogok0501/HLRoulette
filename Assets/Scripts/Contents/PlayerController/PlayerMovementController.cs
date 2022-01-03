using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    Camera cam;
    [SerializeField] Joystick joystick;

    [Header("MovementAnime")]
    private float velocity = 0.0f;
    [SerializeField] float acceleration = 1f;

    [Header("Speed")]
    [SerializeField] float movementSpeed = 5.0f;

    private float horizontalMovement;
    private float verticalMovement;

    Vector3 moveDirection;

    private Rigidbody rb;
    private Animator animator;

    private int velocityHash;

    bool keyPressed = false;

    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        velocityHash = Animator.StringToHash("Velocity");
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalMovement = joystick.Horizontal;
        verticalMovement = joystick.Vertical;

        if (moveDirection != new Vector3(0, 0, 0))
            keyPressed = true;
        else
            keyPressed = false;

        if (keyPressed && velocity < 5.0f)
        {
            velocity += Time.deltaTime * acceleration;
        }
        
        if (!keyPressed && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * acceleration * 5f;
        }

        if (!keyPressed && velocity < 0.0f)
        {
            velocity = 0.0f;
        }

        animator.SetFloat(velocityHash, velocity);
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;        
    }

    private void MovePlayer()
    {      
        rb.AddForce(moveDirection.normalized * (movementSpeed + velocity), ForceMode.Acceleration);
    }
}
