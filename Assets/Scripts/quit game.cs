using UnityEngine;

public class quitgame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quitting game...");

        // Quit the built game
        Application.Quit();

        // Stop Play Mode if running inside the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
