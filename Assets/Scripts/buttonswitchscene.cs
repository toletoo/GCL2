using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonswitchscene : MonoBehaviour
{
    // Name of the scene to load
    public string sceneName;

    // Called by the button
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
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
