using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public bool gameStarted;
    private PlayerController playerController;
    [SerializeField] float gameStartDelay = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        playerController.canMove = false;
    }
    void Start()
    {
        print("Cannot move");
        StartGameCo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGameCo()
    {
        StartCoroutine(StartGame());  
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(gameStartDelay);
        gameStarted = true;
        playerController.canMove = true;
        print("Can move");
    }
}
