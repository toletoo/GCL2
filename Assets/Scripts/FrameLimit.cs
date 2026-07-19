using UnityEngine;

public class FrameLimit : MonoBehaviour
{
    [SerializeField] private int Framecap = 60;

    private void Start()
    {
        // Framecap only works in unity editor
        #if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = Framecap;
        #endif
        
    }
}
