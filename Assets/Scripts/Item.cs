using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (tag)
            {
                case "Hammer":
                    var playerController = collision.GetComponent<PlayerController>();
                    playerController.hammerState = true;
                    Destroy(this.gameObject);
                    break;
                case "Cover":
                    var shield = collision.GetComponentInChildren<Shield>();
                    shield.shieldState = true;
                    Destroy(this.gameObject);
                    break;
                case "TempShield":
                    var shieldState = collision.GetComponent<PlayerController>();
                    shieldState.tempShieldState = true;
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }

        }
    }
}
