using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource scrapPickup;
    [SerializeField] private AudioSource death;

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

    public void Death(AudioClip Death, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(death, spawnTransform.position, Quaternion.identity);

        audioSource.clip = Death;

        audioSource.volume = volume;

        audioSource.Play();

        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);

    }
}
