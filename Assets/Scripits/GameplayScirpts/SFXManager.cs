using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource scrapPickup;
    [SerializeField] private AudioSource collisionDeath;
    [SerializeField] private AudioSource collision;
    [SerializeField] private AudioSource miniSwitcher;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ScrapPickup(AudioClip ScrapPickup, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(scrapPickup, spawnTransform.position, Quaternion.identity);

        audioSource.clip = ScrapPickup;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }

    public void CollisionDeath(AudioClip CollisionDeath, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(collisionDeath, spawnTransform.position, Quaternion.identity);

        audioSource.clip = CollisionDeath;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }

    public void Collision(AudioClip Collision, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(collisionDeath, spawnTransform.position, Quaternion.identity);

        audioSource.clip = Collision;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }

    public void MiniSwitcher(AudioClip MiniSwitcher, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(miniSwitcher, spawnTransform.position, Quaternion.identity);

        audioSource.clip = MiniSwitcher;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }
}
