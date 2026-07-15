using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{ 
    Playing,
    MarioDead
}
public class LevelManager : MonoBehaviour
{

    public bool gameStarted;
    private PlayerController playerController;
    [SerializeField] float gameStartDelay = 1.5f;
    [SerializeField] float respawnDelay = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameState currentGameState;

    private void Awake()
    {
        currentGameState = GameState.Playing;
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

    public void DeathCo()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        currentGameState = GameState.MarioDead;
        yield return new WaitForSeconds(respawnDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
