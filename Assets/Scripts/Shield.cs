using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool shieldState;
    private Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shieldState = false;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.enabled = shieldState;
        collider2D.enabled = shieldState;
        Shielding();
    }
    public void Shielding()
    {
        if (shieldState) 
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            var direction = pivotPoint.position - mousePosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90f;
            pivotPoint.rotation = Quaternion.Euler(0,0, angle);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ParryBarrel")) 
        {
            
        }
    }
}
