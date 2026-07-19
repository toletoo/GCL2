using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private LevelManager levelManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel") || collision.gameObject.CompareTag("ParryBarrel"))
        {
            levelManager.DeathCo(); // mario dies when he collides with barrel
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ParryBarrel") || collision.gameObject.CompareTag("Barrel"))
        {
            levelManager.DeathCo();
        }
    }
}
