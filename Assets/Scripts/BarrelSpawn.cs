using System.Collections;
using UnityEngine;

public class BarrelSpawn : MonoBehaviour
{
    private LevelManager levelManager;
    [SerializeField] GameObject barrel;
    [SerializeField] float barrelSpawnCooldown = 2f;
    private bool canSpawn = false;
    public bool isStun = false;

    // for debugging purposes
    [SerializeField] int maxSpawnCount = 1;
    private int currentSpawnCount = 0;
    private Animator monkeyAnim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
        monkeyAnim = GameObject.FindGameObjectWithTag("Monkey").GetComponent<Animator>();
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.currentGameState != GameState.Playing) return;//added cant spawn when player is dead
        SpawnBarrelCo();
    }

    void SpawnBarrelCo()
    {
        if (levelManager.gameStarted && canSpawn && !isStun)
        {
            StartCoroutine(SpawnBarrel());
        }
    }

    IEnumerator SpawnBarrel()
    {
        if (!gameObject.CompareTag("ParrySpawn"))
        {
            monkeyAnim.SetTrigger("isThrowing");
        }
        if (gameObject.CompareTag("ParrySpawn")) // parry barrel spawns independently of DK throw anim
        {
            Instantiate(barrel, transform.position, Quaternion.identity);
            currentSpawnCount++;
        }
        canSpawn = false;
        yield return new WaitForSeconds(barrelSpawnCooldown);
        if (currentSpawnCount < maxSpawnCount)
        { 
            canSpawn = true;
        }
    }

    public void SpawnBarrelEvent()
    {
        Instantiate(barrel, transform.position, Quaternion.identity);
        currentSpawnCount++;
    }

    public IEnumerator Stun()
    {
        monkeyAnim.SetBool("isStun", true);
        //Cant spawn barrel for set time
        isStun = true;
        yield return new WaitForSeconds(2f);
        monkeyAnim.SetBool("isStun", false);
        isStun = false;
    }
}
