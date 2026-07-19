using System.Collections;
using UnityEngine;

public class ParryBarrelMovement : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody2D rb;
    private Collider2D capsuleCollider;
    public float barrelSpeed;
    public float destroyTime;
    private bool parriedBack = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = FindAnyObjectByType<PlayerController>().transform;
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        var direction = playerTransform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * barrelSpeed;

        Destroy(gameObject, destroyTime);
    }
    public void ParryShoot(Vector3 startPosition, Vector3 endPosition)
    {
        parriedBack = true;//player parry
        var direction = startPosition - endPosition;//uses the collision's variables for parry
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * barrelSpeed;
        Destroy(gameObject, destroyTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Player":
                Destroy(gameObject);
                break;
            case "Shield":
                var script = collision.GetComponent<Shield>();//get shield's script
                var startPos = script.mousePosition;//get mouse position
                var endPos = collision.transform.position;//get shield's position
                ParryShoot(startPos, endPos);
                break;
            case "Monkey":
                if (parriedBack)
                {
                    BarrelSpawn[] barrelSpawner = FindObjectsByType<BarrelSpawn>(FindObjectsSortMode.None);//get all barrel spawners
                    for (int i  = 0; i <  barrelSpawner.Length; i++)
                    {
                        barrelSpawner[i].StartCoroutine(barrelSpawner[i].Stun());//run the stun function
                    }
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }


}
