using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float walkSpeed = 10f;
    public bool isRight = false;
    private float direction;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        direction = isRight ? 1.0f : -1.0f;
        if (Input.GetAxisRaw("Horizontal") > 0) 
        {
            isRight = true;
            rb.linearVelocityX = walkSpeed * direction;
        } else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            isRight = false;
            rb.linearVelocityX = walkSpeed * direction;
        }
        else
        {
            rb.linearVelocityX = 0;
        }
    }
}
