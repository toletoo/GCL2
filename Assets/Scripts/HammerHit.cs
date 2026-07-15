using UnityEngine;

public class HammerHit : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrel") && playerController.hammerState == true)
        {
            Destroy(collision.gameObject);
        }
    }
}
