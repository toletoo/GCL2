using Unity.Hierarchy;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Movement")]
    public float walkSpeed = 10f;
    public float forwardJumpForce = 10f;
    public float jumpForce = 10f;
    public bool isRight = false;
    public bool isGrounded;
    private float direction;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask realGround;
    public float maxJumpDistance;
    public float minJumpDistance;
    private float currenHorizontalInput;
    public bool isJumping = false;
    public bool canMove = false;

    private Vector3 initialLocalScale;
    private Vector3 currentLocalScale;

    [Header("Ladder Related")]
    public bool isLadderNearby = false;
    public bool onLadder = false;
    public float climbSpeed;
    public float minClimbPlatform;//time take to climb platform
    public float maxClimbPlatform;

    [Header("Hammer Related")]
    public bool hammerState = false;

    public float hammerTime;
    private float initialHammerTime;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private LevelManager levelManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialLocalScale = transform.localScale; // store the local scale values
        currentLocalScale = initialLocalScale;
        initialHammerTime = hammerTime; //store the initial hammer time
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        levelManager = FindFirstObjectByType<LevelManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.currentGameState != GameState.Playing) // when mario dies, all movement stopped
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
            
        //Check direction
        direction = isRight ? 1.0f : -1.0f;
        //Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, realGround);
        //Movement
        if (canMove)
        {
            //hammering
            if (hammerState)
            {
                hammerTime -= 0.1f;
                if (hammerTime <= 0)
                {
                    hammerState = false;
                    hammerTime = initialHammerTime;
                }
                anim.SetBool("isHammering", hammerState);
            }
            //Ladder
            if (onLadder)
            {
                //ignore collision for platforms
                Physics2D.IgnoreLayerCollision(7, 6, true);
                if (!isLadderNearby)
                { 
                    //get your fatass over the platform
                    if(minClimbPlatform < maxClimbPlatform)
                    {
                        rb.linearVelocityY = climbSpeed * Time.deltaTime;
                        minClimbPlatform += 0.1f;
                    }
                    else
                    {
                        minClimbPlatform = 0;
                        onLadder = false;
                        rb.gravityScale = 1f;
                    }

                }
                else if (Input.GetAxisRaw("Vertical") > 0)
                {
                    rb.linearVelocityY = (climbSpeed * Time.deltaTime);
                }
                else
                {
                    rb.linearVelocityY = 0.0f;
                }
            }
            //while jumping
            else if (isJumping)
            {
                {
                    //jump for set amount of distance
                    if (minJumpDistance >= maxJumpDistance && isGrounded)
                    {
                        isJumping = false;
                        minJumpDistance = 0;
                    }
                    else if (minJumpDistance < maxJumpDistance && currenHorizontalInput != 0)
                    {
                        rb.linearVelocityX = (forwardJumpForce * direction) * Time.deltaTime;//added value specifically for jump move
                        minJumpDistance += 0.1f;
                    }
                    else
                    {
                        minJumpDistance += 0.1f;
                    }

                }

            }
            else
            {
                //move
                Move();
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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //check if ladder not nearby
        if (collision.CompareTag("Ladder"))
        {
            isLadderNearby = false;
           
        }
    }

    public void Move()
    {
        //Basic Movement
        Physics2D.IgnoreLayerCollision(7, 6, false);
        if (isLadderNearby && Input.GetAxisRaw("Vertical") > 0)
        {
            onLadder = true;
            rb.gravityScale = 0;
            rb.linearVelocityY = climbSpeed * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (!isJumping)
            {
                currenHorizontalInput = Input.GetAxisRaw("Horizontal");
                rb.linearVelocityY = jumpForce;
                isJumping = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector3(-currentLocalScale.x, transform.localScale.y, 1f);
            isRight = true;
            rb.linearVelocityX = (walkSpeed * direction) * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(currentLocalScale.x, transform.localScale.y, 1f);
            isRight = false;
            rb.linearVelocityX = (walkSpeed * direction) * Time.deltaTime;
        }
        else
        {
            rb.linearVelocityX = 0;
        }
    }
}
