using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarrelState
{
    Rolling,
    Falling,
    Ladder,
    Bounce
}
public class BarrelMovement : MonoBehaviour
{
    [SerializeField] float barrelMoveSpeed = 50f;
    [SerializeField] float fallingSpeed = 8;

    // references
    private Transform targetNode;
    private NodeManager nodeManager;
    private LevelManager levelManager;

    private int currentNodeNum = 1;

    //target x and y pos
    private float xPos;
    private float yPos;

    [SerializeField]float rayCastDist = 1.5f;
    private Collider2D barrelCollider;
    private float barrelHalfHeight;

    private int platformMask;
    
    private BarrelState currentState = BarrelState.Rolling;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        platformMask = LayerMask.GetMask("Platform");
    }
    void Start()
    {
        nodeManager = FindFirstObjectByType<NodeManager>();
        targetNode = nodeManager.nodes[currentNodeNum];
        barrelCollider = GetComponent<Collider2D>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.currentGameState != GameState.Playing) return;
        //print(currentState);
        switch (currentState)
        {
            case BarrelState.Rolling:
                UpdateRolling();
                break;

            case BarrelState.Falling:
                UpdateFalling();
                break;

            /*case BarrelState.Ladder:
                UpdateLadder();
                break;

            case BarrelState.Bounce:
                UpdateBounce();
                break;*/
        }
        
        
        //calculate target xpos
        xPos = Mathf.MoveTowards(transform.position.x, targetNode.position.x, barrelMoveSpeed * Time.deltaTime);
        transform.position = new Vector2(xPos, yPos);

        // switch to next target node when barrel gets close enough to current one
        if (Vector2.Distance(transform.position, targetNode.position) < 0.08f)
        {
            if (currentNodeNum < nodeManager.nodes.Count - 1)
            {
                currentNodeNum++;
                targetNode = nodeManager.nodes[currentNodeNum];

                if (currentState == BarrelState.Rolling) // switch between rolling and falling when dropping between girders
                {
                    currentState = BarrelState.Falling;
                }
                else
                {
                    currentState = BarrelState.Rolling;
                }
            }
            else
            {
                Destroy(gameObject);
            }

        }
        
        
    }

    private void UpdateRolling()
    {
        barrelHalfHeight = barrelCollider.bounds.extents.y;
        Vector2 origin = (Vector2)transform.position + barrelCollider.offset;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayCastDist, platformMask);
        Debug.DrawRay(transform.position, Vector2.down * rayCastDist, Color.red);
        if (hit)
        {
            yPos = hit.point.y + barrelHalfHeight; // snap y offset to staggered steel beams on girder

        }
    }

    private void UpdateFalling()
    {
        yPos = Mathf.MoveTowards(transform.position.y, targetNode.position.y, fallingSpeed * Time.deltaTime); // calculate target y pos
    }
}
