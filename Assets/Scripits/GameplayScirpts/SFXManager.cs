using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ScrapPickup(AudioClip ScrapPickup, Transform spawnTransform)
    {

    }
}
