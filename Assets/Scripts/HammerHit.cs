using UnityEngine;

public class HammerHit : MonoBehaviour
{
    private PlayerController playerController;
    private Collider2D boxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.hammerState == true)
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrel") && playerController.hammerState == true)
        {
            Destroy(collision.gameObject);
        }
    }
}
