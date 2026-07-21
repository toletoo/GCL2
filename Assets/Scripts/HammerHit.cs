using UnityEngine;

public class HammerHit : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject hitbox;
    public float radius;
    public LayerMask layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        hitbox = GameObject.FindGameObjectWithTag("AttackHitbox");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Hit()
    {
        Collider2D[] barrels = Physics2D.OverlapCircleAll(hitbox.transform.position, radius, layerMask);

        foreach (Collider2D col in barrels)
        {
            Destroy(col.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitbox.transform.position, radius);
    }
}
