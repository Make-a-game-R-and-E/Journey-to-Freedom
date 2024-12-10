using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    // Input actions
    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector2 moveInput;
    private bool jumpPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Create the Move action (Vector2)
        moveAction = new InputAction("Move", InputActionType.Value, null, null, null, "Vector2");
        // Add a 2DVector composite binding for WASD movement
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // Create the Jump action (Button)
        jumpAction = new InputAction("Jump", InputActionType.Button, "<Keyboard>/space");

        // Subscribe to jump action events
        jumpAction.performed += OnJumpPerformed;
        jumpAction.canceled += OnJumpCanceled;
    }

    private void OnEnable()
    {
        // Enable the input actions
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions
        moveAction.Disable();
        jumpAction.Disable();
    }

    private void Update()
    {
        CheckGrounded();

        // Read movement input continuously
        moveInput = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement
        float xVelocity = moveInput.x * moveSpeed;
        rb.linearVelocity = new Vector2(xVelocity, rb.linearVelocity.y);

        // Apply jump if needed
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void CheckGrounded()
    {
        // Simple ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Jump action callbacks
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jumpPressed = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        jumpPressed = false;
    }
}
