using System.Collections;
using UnityEngine;

public class BarrelSpawn : MonoBehaviour
{
    private LevelManager levelManager;
    [SerializeField] GameObject barrel;
    [SerializeField] float barrelSpawnCooldown = 2f;
    private bool canSpawn = false;

    // for debugging purposes
    [SerializeField] int maxSpawnCount = 1;
    private int currentSpawnCount = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBarrelCo();
    }

    void SpawnBarrelCo()
    {
        if (levelManager.gameStarted && canSpawn)
        {
            StartCoroutine(SpawnBarrel());
        }
    }

    IEnumerator SpawnBarrel()
    {
        Instantiate(barrel,gameObject.transform.position,Quaternion.identity); // spawn barrels every x seconds
        currentSpawnCount++;
        canSpawn = false;
        yield return new WaitForSeconds(barrelSpawnCooldown);
        if (currentSpawnCount < maxSpawnCount)
        { 
            canSpawn = true;
        }
    }
}
