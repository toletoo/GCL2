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
    public AudioClip jingle;
    public AudioClip song;
    public AudioClip death;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameState currentGameState;

    private void Awake()
    {
        currentGameState = GameState.Playing;
        playerController = FindFirstObjectByType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        playerController.canMove = false;
    }
    void Start()
    {
        audioSource.PlayOneShot(jingle);
        print("Cannot move");
        StartGameCo();
    }

    void StartGameCo()
    {
        StartCoroutine(StartGame());  
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(gameStartDelay); // delays start of game
        gameStarted = true;
        playerController.canMove = true;
        audioSource.clip = song;
        audioSource.Play();
        print("Can move");
    }

    public void DeathCo()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        currentGameState = GameState.MarioDead;
        yield return new WaitForSeconds(respawnDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
