using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmeMovementController : MonoBehaviour
{
    [SerializeField] Joystick joystick;

    [Header("MovementAnime")]
    private float velocity = 0.0f;
    [SerializeField] float acceleration = 1f;

    [Header("Speed")]
    [SerializeField] float movementSpeed = 5.0f;
    [SerializeField] float jumpForce = 500.0f;

    private float horizontalMovement;
    private float verticalMovement;

    Vector2 originalPos;

    Vector2 moveDirection;

    Vector3 scale;

    private Rigidbody2D rb2d;
    private Animator anim;

    public Collider2D groundpoundHitbox;
    int jumpCount = 0;
    int maxJump = 1;

    bool keyPressed = false;

    private void Awake()
    {
        originalPos = transform.position;
    }

    private void OnEnable()
    {
        transform.position = originalPos;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        anim = GetComponent<Animator>();
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

        if (moveDirection != new Vector2(0, 0))
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

        moveDirection = transform.right * horizontalMovement;
    }

    private void MovePlayer()
    {
        rb2d.velocity = new Vector2(moveDirection.x * (movementSpeed + velocity) * 10f, rb2d.velocity.y);

        if (rb2d.velocity.x > 0)
        {
            scale.Set(-1.0f, 1.0f, 1.0f);
            transform.localScale = scale;
        }
        else
        {
            scale.Set(1.0f, 1.0f, 1.0f);
            transform.localScale = scale;
        }
    }

    public void Jump()
    {
        if(jumpCount > 0)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            anim.SetBool("is Jump", true);
            jumpCount--;
        }
    }

    public void Groundpound()
    {
        if(anim.GetBool("is Jump"))
        {
            rb2d.AddForce(new Vector2(0f, -jumpForce));
            anim.SetTrigger("GroundpoundTri");
            groundpoundHitbox.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ame_Gorund" || collision.gameObject.tag == "Ame_Box")
        {
            anim.SetBool("is Jump", false);
            groundpoundHitbox.gameObject.SetActive(false);
            jumpCount = maxJump;
        }
    }
}
