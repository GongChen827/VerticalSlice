using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuUI : MonoBehaviour
{
    [Header("Restart Sound")]
    [SerializeField] private AudioClip uiClickSFX;

    [Range(0f, 1f)]
    [SerializeField] private float uiClickVolume = 0.8f;

    [Header("Restart Delay")]
    [SerializeField] private float restartDelay = 0.25f;

    private bool isRestarting = false;

    public void RestartScene()
    {
        if (isRestarting) return;

        isRestarting = true;

        Time.timeScale = 1f;

        PlayClickSound();

        StartCoroutine(RestartAfterDelay());
    }

    private void PlayClickSound()
    {
        if (uiClickSFX == null)
        {
            Debug.LogWarning("DeathMenuUI: UI Click SFX is missing.");
            return;
        }

        GameObject tempAudioObject = new GameObject("Temp_DeathMenu_Click_Sound");
        DontDestroyOnLoad(tempAudioObject);

        AudioSource audioSource = tempAudioObject.AddComponent<AudioSource>();
        audioSource.clip = uiClickSFX;
        audioSource.volume = uiClickVolume;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;

        audioSource.Play();

        Destroy(tempAudioObject, uiClickSFX.length + 0.1f);
    }

    private IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSecondsRealtime(restartDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}