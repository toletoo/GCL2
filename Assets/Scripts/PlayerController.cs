using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float walkSpeed = 10f;
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
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            
        } else if (Input.GetKeyDown(KeyCode.A))
        {

        }
    }
}
