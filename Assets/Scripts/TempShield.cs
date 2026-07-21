using UnityEngine;

public class TempShield : MonoBehaviour
{
    public float visibleTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Destroy(gameObject, visibleTimer);
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
