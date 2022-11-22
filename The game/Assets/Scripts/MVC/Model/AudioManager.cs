using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource pauseMenuSource;
    [SerializeField] private string backgroundMusic;
    [SerializeField] private string pauseMenuMusic;
    private AudioSource activeMusic;
    private AudioSource inactiveMusic;
    private bool crossFading;
    private float musicVolume = 1;

    public ManagerStatus status { get; private set; }

    //обеспечивает плавный переход между музыкой
    public float crossFadeRate = 1.5f;

    public void Startup()
    {
        activeMusic = pauseMenuSource;
        inactiveMusic = backgroundSource;
        status = ManagerStatus.Started;
    }

    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = value;
            if (backgroundSource != null && !crossFading)
            {
                backgroundSource.volume = musicVolume;
                pauseMenuSource.volume = musicVolume;
            }
        }
    }

    public void StopAllMusic()
    {
        backgroundSource.Stop();
        pauseMenuSource.Stop();
    }

    public void PlayBGMusic() => PlayMusic((AudioClip)Resources.Load($"Music/{backgroundMusic}"));

    public void PlayPMenuMusic() => PlayMusic((AudioClip)Resources.Load($"Music/{pauseMenuMusic}"));

    private void PlayMusic(AudioClip music)
    {
        if(crossFading) return;
        StartCoroutine(CrossFadeMusic(music));
    }

    private IEnumerator CrossFadeMusic(AudioClip clip)
    {
        crossFading = true;

        inactiveMusic.clip = clip;
        inactiveMusic.volume = 0;
        inactiveMusic.Play();

        float scaledRate = crossFadeRate * MusicVolume;
        while (activeMusic.volume > 0)
        {
            activeMusic.volume -= scaledRate * Time.deltaTime;
            inactiveMusic.volume += scaledRate * Time.deltaTime;
            yield return null;
        }

        AudioSource temp = activeMusic;
        activeMusic = inactiveMusic;
        activeMusic.volume = MusicVolume;
        inactiveMusic = temp;
        inactiveMusic.Stop();

        crossFading = false;
    }
}
