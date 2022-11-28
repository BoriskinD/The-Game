using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float jumpForce = 10.0f;
    public float xLocalScale = 10f;
    public float bottomBoundary = -5f;
    public GameObject canvas;
    public Canvas healthBar;

    private Rigidbody2D rb2D;
    private Animator animator;
    //private BoxCollider2D boxCollider;
    private PlayerCombat playerCombat;
    private bool isJumping = false;
    private bool facingRight;

    private void Awake()
    {
        //boxCollider = GetComponent<BoxCollider2D>();
        playerCombat = GetComponent<PlayerCombat>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Messenger.AddListener(GameEvent.GAME_PAUSED, OnGamePaused);
        Messenger.AddListener(GameEvent.GAME_UNPAUSED, OnGameUnPaused);
        Messenger.AddListener(GameEvent.PLAYER_DIED, OnPlayerDied);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_PAUSED, OnGamePaused); 
        Messenger.RemoveListener(GameEvent.GAME_UNPAUSED, OnGameUnPaused);
        Messenger.RemoveListener(GameEvent.PLAYER_DIED, OnPlayerDied);
    } 

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.y < bottomBoundary)
            transform.position = new (transform.position.x, bottomBoundary);

        if (horizontalInput > 0.01f && !facingRight)
        {
            facingRight = !facingRight;
            Flip();
        }
        else if (horizontalInput < -0.01f && facingRight)
        {
            facingRight = !facingRight;
            Flip();
        }

        if (horizontalInput != 0)
        {
            transform.Translate(horizontalInput * movementSpeed * Time.deltaTime * transform.right);
            animator.SetFloat("f_walkSpeed", 0.6f);
        }
        else animator.SetFloat("f_walkSpeed", 0.1f);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            animator.SetBool("b_isJumping", isJumping);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        animator.SetBool("b_isJumping", isJumping);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Dead Zone"))
        {
            playerCombat.enabled = false;
            enabled = false;
            Messenger.Broadcast(GameEvent.PLAYER_DIED);
        }
    }

    private void Flip()
    {
        Vector3 playersRotation = transform.eulerAngles;
        Vector3 canvasRotation = canvas.transform.eulerAngles;
        canvasRotation.y = 0;

        if (facingRight) playersRotation.y = 0;
        else playersRotation.y = 180; 

        transform.eulerAngles = playersRotation;
        canvas.transform.eulerAngles = canvasRotation;
    }

    private void OnGamePaused()
    {
        animator.enabled = false;
        enabled = false;
    }
    private void OnGameUnPaused()
    {
        animator.enabled = true;
        enabled = true;
    }

    private void OnPlayerDied()
    {
        //boxCollider.enabled = false;
        healthBar.gameObject.SetActive(false);
        enabled = false;
    }
}