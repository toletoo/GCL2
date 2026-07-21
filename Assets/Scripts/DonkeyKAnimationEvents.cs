using UnityEngine;

public class DonkeyKAnimationEvents : MonoBehaviour
{
    [SerializeField] private BarrelSpawn barrelSpawner;

    // syncs barrel spawning to animation
    public void SpawnBarrel()
    {
        barrelSpawner.SpawnBarrelEvent();
    }
}
