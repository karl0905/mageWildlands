using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer mageSpriteRenderer;
    private float Move;
    private int jumpsRemaining;
    public float speed;
    public int maxJumps = 1;
    public bool isFacingRight;
    public float jumpHeight;


    void Start()
    {
        animator = GetComponent<Animator>();
        mageSpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        jumpsRemaining = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(Move * speed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            jumpsRemaining--;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("isAttacking_1", true);
        }
        else
        {
            animator.SetBool("isAttacking_1", false);
        }
    }

    void FixedUpdate()
    {
        if (Move >= 0.01f || Move <= -0.01f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (!isFacingRight && Move > 0f)
        {
            Flip();
        }
        else if (isFacingRight && Move < 0f)
        {
            Flip();
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
            jumpsRemaining = maxJumps;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", true);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
