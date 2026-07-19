using UnityEngine;

public class ParryBarrelMovement : MonoBehaviour
{
    private Transform playerPosition;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = FindAnyObjectByType<PlayerController>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
