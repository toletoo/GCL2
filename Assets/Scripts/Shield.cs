using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool shieldState;
    public float shieldDuration;
    private float shieldSavedDuration;
    private Collider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform pivotPoint;//get pivot parent
    [SerializeField] private Camera cam;//get camera
    public Vector3 mousePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shieldSavedDuration = shieldDuration;
    }

    // Update is called once per frame
    void Update()
    {
        //set the visibility and boxcollider to the shield state
        spriteRenderer.enabled = shieldState;
        boxCollider.enabled = shieldState;

    }
    private void FixedUpdate()
    {
        Shielding();
        ShieldCountdown();
    }
    public void Shielding()
    {
        if (shieldState) 
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);//get mouse position
            mousePosition.z = 0f;
            var direction = pivotPoint.position - mousePosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90f;//get angle
            pivotPoint.rotation = Quaternion.Euler(0,0, angle);//rotate to mouse
        }
    }
    public void ShieldCountdown()
    {
        //how long 
        if(shieldDuration > 0 && shieldState)
        {
            shieldDuration -= 0.1f;
        }
        else
        {
            shieldState = false;
            shieldDuration = shieldSavedDuration;
        }
    }
}
