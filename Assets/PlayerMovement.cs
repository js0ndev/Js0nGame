using System.Collections;
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

    [Header("Pés no chão")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("GameOver")]
    [SerializeField] private float deathLevel = -10f;
    [SerializeField] private GameObject gameOverPanel;
    private bool isDead;

    [Header("Ataque")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject attackSprite;
    private bool isAttacking;


    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrounded;
    private float horizontalInput;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }
    public void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void Update()
    {
        InverterPlayer();
        footGrounded();
        HandleInput();
        HandleShift();
        HandleJump();
        CheckDeath();
        HandleAttack();
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
    public void footGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            checkRadius,
            groundLayer
        );
    }
    public void HandleJump()
    {
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            forceJump = shiftPressed ? runJump : walkJump;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forceJump);
        }
    }
    public void CheckDeath()
    {
        if(isDead) return;

        if (rb.position.y < deathLevel)
        {
        isDead = true;
        Debug.Log("Morreu");
        GameOver();
        }
    }
    public void GameOver()
    {
        if(isDead)
        {
            Time.timeScale = 0f;
            Debug.Log("Game Over");
            gameOverPanel.SetActive(true);
        }
    }
    public void InverterPlayer()
    {
        if(horizontalInput > 0)
        transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        if(horizontalInput < 0)
        transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
    }
    public void HandleAttack()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.C) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        isAttacking = true;
        Debug.Log("Atacou");
        attackSprite.SetActive(true);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRadius,
            enemyLayer
        );
        foreach(Collider2D enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();

if(enemyScript != null)
{
    enemyScript.Die();
}
        }
        yield return new WaitForSeconds(0.2f);
        attackSprite.SetActive(false);
        isAttacking = false;
        Debug.Log("Parou de Atacar");
    }

}
