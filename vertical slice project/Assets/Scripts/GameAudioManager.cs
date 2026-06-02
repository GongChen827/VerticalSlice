using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip explorationMusic;
    [SerializeField] private AudioClip bossMusic;

    [Header("Ambience")]
    [SerializeField] private AudioClip oceanWindAmbience;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip landingSFX;
    [SerializeField] private AudioClip powerupCollectSFX;
    [SerializeField] private AudioClip lightBurstSFX;
    [SerializeField] private AudioClip playerHurtSFX;
    [SerializeField] private AudioClip playerDeathSFX;
    [SerializeField] private AudioClip enemyHitSFX;
    [SerializeField] private AudioClip enemyDeathSFX;
    [SerializeField] private AudioClip barrierBreakSFX;
    [SerializeField] private AudioClip exitOpenSFX;
    [SerializeField] private AudioClip winSFX;
    [SerializeField] private AudioClip uiClickSFX;

    [Header("Volume")]
    [Range(0f, 1f)] [SerializeField] private float musicVolume = 0.45f;
    [Range(0f, 1f)] [SerializeField] private float ambienceVolume = 0.35f;
    [Range(0f, 1f)] [SerializeField] private float sfxVolume = 0.85f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.playOnAwake = false;
            musicSource.volume = musicVolume;
            musicSource.spatialBlend = 0f;
        }

        if (ambienceSource != null)
        {
            ambienceSource.loop = true;
            ambienceSource.playOnAwake = false;
            ambienceSource.volume = ambienceVolume;
            ambienceSource.spatialBlend = 0f;
        }

        if (sfxSource != null)
        {
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
            sfxSource.volume = sfxVolume;
            sfxSource.spatialBlend = 0f;
        }
    }

    private void Start()
    {
        PlayExplorationMusic();
        PlayOceanWindAmbience();
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null)
        {
            return;
        }

        if (musicSource.clip == clip && musicSource.isPlaying)
        {
            return;
        }

        musicSource.clip = clip;
        musicSource.volume = musicVolume;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayExplorationMusic()
    {
        PlayMusic(explorationMusic);
    }

    public void PlayBossMusic()
    {
        PlayMusic(bossMusic);
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void PlayOceanWindAmbience()
    {
        if (ambienceSource == null || oceanWindAmbience == null)
        {
            return;
        }

        ambienceSource.clip = oceanWindAmbience;
        ambienceSource.volume = ambienceVolume;
        ambienceSource.loop = true;
        ambienceSource.Play();
    }

    public void StopAmbience()
    {
        if (ambienceSource != null)
        {
            ambienceSource.Stop();
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayJumpSFX()
    {
        PlaySFX(jumpSFX);
    }

    public void PlayLandingSFX()
    {
        PlaySFX(landingSFX);
    }

    public void PlayPowerupCollectSFX()
    {
        PlaySFX(powerupCollectSFX);
    }

    public void PlayLightBurstSFX()
    {
        PlaySFX(lightBurstSFX);
    }

    public void PlayPlayerHurtSFX()
    {
        PlaySFX(playerHurtSFX);
    }

    public void PlayPlayerDeathSFX()
    {
        PlaySFX(playerDeathSFX);
    }

    public void PlayEnemyHitSFX()
    {
        PlaySFX(enemyHitSFX);
    }

    public void PlayEnemyDeathSFX()
    {
        PlaySFX(enemyDeathSFX);
    }

    public void PlayBarrierBreakSFX()
    {
        PlaySFX(barrierBreakSFX);
    }

    public void PlayExitOpenSFX()
    {
        PlaySFX(exitOpenSFX);
    }

    public void PlayWinSFX()
    {
        PlaySFX(winSFX);
    }

    public void PlayUIClickSFX()
    {
        PlaySFX(uiClickSFX);
    }
}