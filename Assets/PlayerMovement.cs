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
        HandleShift();
        HandleJump();
    }

    public void FixedUpdate()
    {
        HandleMovement();
    }

    public void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
    public void HandleMovement()
    {
        moveSpeed = shiftPressed ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveSpeed * horizontalInput, rb.linearVelocity.y);
    }
    public void HandleShift()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            shiftPressed = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            shiftPressed = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            isGrounded = true;
            Debug.Log("Tocou o chão");
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            isGrounded = false;
            Debug.Log("Saiu do chão");
        }
    }
    public void HandleJump()
    {
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            forceJump = shiftPressed ? runJump : walkJump;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forceJump);
        }
    }
}
