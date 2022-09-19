using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float jumpForce = 10.0f;
    public float xLocalScale = 10f;
    public GameObject canvas;

    private Rigidbody2D playersRb;
    private Animator playerAnim;
    private bool isJumping = false;
    private bool facingRight;

    private void Start()
    {
        playersRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

     private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

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
            transform.Translate(transform.right * movementSpeed * Time.deltaTime * horizontalInput);
            playerAnim.SetFloat("f_walkSpeed", 0.6f);
        }
        else playerAnim.SetFloat("f_walkSpeed", 0.1f);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            playersRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            playerAnim.SetBool("b_isJumping", isJumping);
        }  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        playerAnim.SetBool("b_isJumping", isJumping);
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
}