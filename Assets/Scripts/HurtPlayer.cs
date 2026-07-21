using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private LevelManager levelManager;
    private PlayerController playerController;
    private Shield shield;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
        playerController = GetComponent<PlayerController>();
        shield =  FindAnyObjectByType<Shield>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel") || collision.gameObject.CompareTag("ParryBarrel"))
        {
            if (playerController.tempShieldState)
            {
                playerController.tempShieldState = false;
                Destroy(collision.gameObject);
            }
            else if (!playerController.hammerState)
            {
                levelManager.DeathCo(); // mario dies when he collides with barrel
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel") || collision.gameObject.CompareTag("ParryBarrel"))
        {
            if (playerController.tempShieldState)
            {
                playerController.tempShieldState = false;
                Destroy(collision.gameObject);
            }
            else if (!playerController.hammerState)
            {
                levelManager.DeathCo(); // mario dies when he collides with barrel
            }
        }
        else if (collision.CompareTag("Void"))
        {
            levelManager.DeathCo();
        }
        if (collision.gameObject.CompareTag("SpikeBarrel") && playerController.hammerState || collision.gameObject.CompareTag("SpikeBarrel") && shield.shieldState)
        {

            shield.shieldState = false;
            playerController.hammerTime = 0;
            Destroy(collision.gameObject);

        }
    }
}
