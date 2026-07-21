using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TempShieldSpawner : MonoBehaviour
{
    public Vector3 randomPos;
    [SerializeField] GameObject tempShieldPrefab;
    public float shieldSpawnCooldown;
    public Transform[] points;
    public GameObject[] areas;
    private bool canSpawn = false;
    private LevelManager levelManager;
    // Update is called once per frame
    private void Start()
    {
        points = GetComponentsInChildren<Transform>();
        levelManager = FindAnyObjectByType<LevelManager>();
        canSpawn = true;
    }
    void Update()
    {
        if (levelManager.currentGameState != GameState.Playing) return;//added cant spawn when player is dead
        StartSpawnCo();

    }
    void StartSpawnCo()
    {
        if (levelManager.gameStarted && canSpawn)
        {
            StartCoroutine(SpawnRandom());
        }
    }
    IEnumerator SpawnRandom()
    {
        var randNo = Random.Range(0 , points.Length);
        randomPos = points[randNo].position;
        print(points[randNo].position);
        Instantiate(tempShieldPrefab, randomPos, Quaternion.identity);
        canSpawn = false;
        yield return new WaitForSeconds(shieldSpawnCooldown);
        canSpawn = true;
    }
}
