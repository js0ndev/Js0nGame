using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 6f;
    private float moveSpeed;
    private bool shiftPressed;

    [Header("Pulo")]
    [SerializeField] private float walkJump = 4f;
    [SerializeField] private float runJump = 6f;
    private float forceJump;

    [Header("GameOver")]
    [SerializeField] private float deathLevel = -10f;
    private bool isDead;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        HandleInput();
    }

    public void FixedUpdate()
    {
        
    }

    public void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
    public void HandleMovement()
    {
        rb.linearVelocity = new Vector2(moveSpeed * horizontalInput, rb.linearVelocity.x);
    }
}
