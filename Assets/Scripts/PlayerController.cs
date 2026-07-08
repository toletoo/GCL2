using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Movement")]
    public float walkSpeed = 10f;
    public float jumpForce = 10f;
    public bool isRight = false;
    public bool isGrounded;
    private float direction;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask realGround;
    public float maxJumpDistance;
    public float minJumpDistance;
    public bool isJumping = false;
    public bool canMove = false;

    [Header("Ladder Related")]
    public bool isLadderNearby = false;
    public bool onLadder = false;
    public float climbSpeed;

    [Header("Hammer Related")]
    public bool hammerState = false;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //Check direction
        direction = isRight ? 1.0f : -1.0f;
        //Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, realGround);
        //Movement
        if (canMove)
        {
            //Ladder
            if (onLadder)
            {
                //ignore collision for platforms
                Physics2D.IgnoreLayerCollision(7, 6, true);
                if (!isLadderNearby)
                {
                    onLadder = false;
                    rb.gravityScale = 1f;
                }
                else if (Input.GetAxisRaw("Vertical") > 0)
                {
                    rb.linearVelocityY = climbSpeed;
                }
                else
                {
                    rb.linearVelocityY = 0.0f;
                }
            }
            //Jumping
            else if (isJumping)
            {
                {
                    //jump for set amount of distance
                    if (minJumpDistance >= maxJumpDistance && isGrounded)
                    {
                        isJumping = false;
                        minJumpDistance = 0;
                    }
                    else
                    {
                        rb.linearVelocityX = (walkSpeed * 0.5f) * direction;
                        minJumpDistance += 0.1f;
                    }

                }

            }
            else
            {
                //Basic Movement
                Physics2D.IgnoreLayerCollision(7, 6, false);
                if (isLadderNearby && Input.GetAxisRaw("Vertical") > 0)
                {
                    onLadder = true;
                    rb.gravityScale = 0;
                    rb.linearVelocityY = climbSpeed;
                }
                else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    if (!isJumping)
                    {
                        rb.linearVelocityY = jumpForce;

                        isJumping = true;
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    transform.localScale = new Vector3(-0.6f, transform.localScale.y, -0.6f);
                    isRight = true;
                    rb.linearVelocityX = (walkSpeed * direction);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    transform.localScale = new Vector3(0.6f, transform.localScale.y, 0.6f);
                    isRight = false;
                    rb.linearVelocityX = (walkSpeed * direction);
                }
                else
                {
                    rb.linearVelocityX = 0;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if ladder is nearby
        if (collision.CompareTag("Ladder"))
        {
            isLadderNearby = true;
        }
        //check for hammer
        if (collision.CompareTag("Hammer"))
        {
            hammerState = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //check if ladder not nearby
        if (collision.CompareTag("Ladder"))
        {
            isLadderNearby = false;
           
        }
    }

}
