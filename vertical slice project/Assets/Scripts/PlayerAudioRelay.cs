using UnityEngine;

public class PlayerAudioRelay : MonoBehaviour
{
    public void PlayJumpSFX()
    {
        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayJumpSFX();
        }
    }

    public void PlayLightBurstSFX()
    {
        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayLightBurstSFX();
        }
    }

    public void PlayPowerupCollectSFX()
    {
        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayPowerupCollectSFX();
        }
    }

    public void PlayUIClickSFX()
    {
        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlayUIClickSFX();
        }
    }
}