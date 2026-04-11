using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource scrapPickup;
    [SerializeField] private AudioSource collisionDeath;
    [SerializeField] private AudioSource collision;
    [SerializeField] private AudioSource miniSwitcher;
    [SerializeField] private AudioSource repair;
    [SerializeField] private AudioSource respawn;
    [SerializeField] private AudioSource deny;
    [SerializeField] private AudioSource warning;
    [SerializeField] private AudioSource trapDeath;
    [SerializeField] private AudioSource dialogue;

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

    public void Repair(AudioClip Repair, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(repair, spawnTransform.position, Quaternion.identity);

        audioSource.clip = Repair;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }

    public void Respawn(AudioClip Respawn, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(respawn, spawnTransform.position, Quaternion.identity);

        audioSource.clip = Respawn;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }

    public void Deny(AudioClip Deny, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(deny, spawnTransform.position, Quaternion.identity);

        audioSource.clip = Deny;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }

    public void Warning(AudioClip Warning, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(warning, spawnTransform.position, Quaternion.identity);

        audioSource.clip = Warning;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }

    public void TrapDeath(AudioClip TrapDeath, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(trapDeath, spawnTransform.position, Quaternion.identity);

        audioSource.clip = TrapDeath;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }

    public void Dialogue(AudioClip Dialogue, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(dialogue, spawnTransform.position, Quaternion.identity);

        audioSource.clip = Dialogue;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }
}
