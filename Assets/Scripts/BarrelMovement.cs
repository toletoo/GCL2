using System.Collections;
using UnityEngine;

public class BarrelMovement : MonoBehaviour
{
    private Rigidbody2D barrelRb;
    [SerializeField] float barrelMoveSpeed = 50f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       barrelRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector2.right * barrelMoveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        
    }
}
