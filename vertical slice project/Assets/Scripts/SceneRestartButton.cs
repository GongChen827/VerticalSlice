using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestartButton : MonoBehaviour
{
    [SerializeField] private float restartDelayAfterClick = 0.2f;

    private bool isRestarting = false;

    public void RestartScene()
    {
        if (isRestarting) return;

        isRestarting = true;

        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayUIClickSFX();
        }

        StartCoroutine(RestartSceneAfterDelay());
    }

    private IEnumerator RestartSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(restartDelayAfterClick);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}