using Unity.Hierarchy;
using Unity.VisualScripting;
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
    public bool jump = false;
    public bool climb = false;
    public bool canMove = false;
    private Vector3 moveInput;

    private Vector3 initialLocalScale;
    private Vector3 currentLocalScale;

    [Header("Ladder Related")]
    public bool isLadderNearby = false;
    public bool onLadder = false;
    public float climbSpeed;


    [Header("Hammer Related")]
    public bool hammerState = false;
    public float hammerWalkSpeed = 10f;
    public float hammerTime;
    public float initialHammerTime;
    private Rigidbody2D rb;
    private Collider2D capsuleCollider;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private Transform platformGap; //Platform to climb up to
    [Header("TempShield Related")]
    public bool tempShieldState = false;
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
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        levelManager = FindFirstObjectByType<LevelManager>();

    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(x, 0f, z).normalized;
        //Check direction
        direction = isRight ? 1.0f : -1.0f;
        TempShield();
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
        //Movement
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                climb = true;
            }
        
    }
    public void FixedUpdate()
    {
        if (levelManager.currentGameState != GameState.Playing) // when mario dies, all movement stopped
        {
            hammerTime = 0;
            rb.linearVelocity = Vector3.zero;
            return;
        }
        //Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, realGround);
        if (canMove)
        {

                Ladder();
                //move
                Jump();
                Move();
                anim.SetFloat("isMoving", Mathf.Abs(rb.linearVelocityX));
                anim.SetBool("isGrounded", isGrounded);



        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Check if ladder is nearby
        if (collision.CompareTag("Ladder"))
        {
            if (collision.TryGetComponent<BrokenLadder>(out BrokenLadder brokenLadder) && brokenLadder.isBroken)
            {
                brokenLadder.canRepair = true;
            }
            else
            {
                isLadderNearby = true;
            }
        }
        if (collision.CompareTag("Platform"))
        {
            platformGap = collision.transform;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        //check if ladder not nearby
        if (collision.CompareTag("Ladder"))
        {
            if (collision.TryGetComponent<BrokenLadder>(out BrokenLadder brokenLadder) && brokenLadder.isBroken)
            {
                brokenLadder.canRepair = false;
            }
            else
            {                
                isLadderNearby = false;
            }
           
        }
    }

    void TempShield()
    {
        if (tempShieldState)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
    public void Move()
    {
        //Basic Movement

         if (moveInput.x > 0 && !onLadder)
        {
            transform.localScale = new Vector3(-currentLocalScale.x, transform.localScale.y, 1f);
            isRight = true;
            if (hammerState)
            {
                rb.linearVelocityX = (hammerWalkSpeed * moveInput.x) * Time.deltaTime;
            }
            else
            {
                rb.linearVelocityX = (walkSpeed * moveInput.x) * Time.deltaTime;
            }

        }
        else if (moveInput.x < 0 && !onLadder)
        {
            transform.localScale = new Vector3(currentLocalScale.x, transform.localScale.y, 1f);
            isRight = false;
            if (hammerState)
            {
                rb.linearVelocityX = (hammerWalkSpeed * moveInput.x) * Time.deltaTime;
            }
            else
            {
                rb.linearVelocityX = (walkSpeed * moveInput.x) * Time.deltaTime;
            }
        }
        else
        {
            rb.linearVelocityX = 0;
        }
    }
    public void Jump()
    {
            if (jump)
            {
            capsuleCollider.isTrigger = true;
            rb.linearVelocityY = jumpForce;
            capsuleCollider.isTrigger = false;
            jump = false;

        }
    }
    public void Ladder()
    {
        if (climb && !onLadder)
        {
            if (isLadderNearby) 
            {
                //if ladder nearby
                capsuleCollider.isTrigger = true;//dont collide when going through platform
                onLadder = true;
                rb.gravityScale = 0;
            }
        }
        else if (onLadder)
        {
            //when on ladder
            rb.linearVelocityX = 0;//stop left right movement

            if (!isLadderNearby)
            {
                capsuleCollider.isTrigger = false;
                onLadder = false;
                climb = false;
                rb.gravityScale = 1f;//come back to earth bro

            }
                else if (moveInput.z > 0)
                {
                //movement on ladder
                    rb.linearVelocityY = (climbSpeed * moveInput.z) * Time.deltaTime;
            }
            else
            {
                rb.linearVelocityY = 0;//stop moving when no input
            }
        }
        climb = false;

    }

}
