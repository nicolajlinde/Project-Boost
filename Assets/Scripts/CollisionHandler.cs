using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayBySeconds = 1f;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip successMessage;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource audioSource;
    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly!");
                break;
            case "Finish":
                Debug.Log("Finish!");
                NextLevelSequence();
                break;
            default:
                StartCrashSequence();
                Debug.Log("I bumped into something.");
                break;
        }
    }

    void StartCrashSequence()
    {
        // TODO: Add particle FX
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(deathExplosion, 0.2f);
        deathParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayBySeconds);
    }

    void NextLevelSequence()
    {
        // TODO: Add particle FX
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successMessage, 0.2f);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayBySeconds);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int loadNextScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = loadNextScene + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
