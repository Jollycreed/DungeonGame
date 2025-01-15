using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;    // Singleton instance
    public AudioSource audioSource;         // Reference to the AudioSource component

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this GameObject persistent across scenes
        }
        else
        {
            Destroy(gameObject);            // Destroy duplicate instances
        }
    }

    private void Start()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Call this method to change the background music dynamically
    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip != newClip)
        {
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}
