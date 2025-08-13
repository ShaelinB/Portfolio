using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip playerAttack;
    public AudioClip playerBlock;
    public AudioClip playerLand;
    public AudioClip robotBattle;
    public AudioClip robotLaser;
    public AudioClip robotMissile;
    public AudioClip robotShock;
    public AudioClip robotSwipe;
    public AudioClip tomatoBattle;
    public AudioClip tomatoKick;
    public AudioClip tomatoLand;
    public AudioClip tomatoRoll;
    public AudioClip tomatoThrow;

    public int scene;
    // Start is called before the first frame update
    void Start()
    {
        SetClipsToSource();
        PlayMusicForScene(scene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void PlayMusicForScene(int scene)
    {
        if (scene <= 4)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
        if (scene == 5)
        {
            musicSource.clip = tomatoBattle;
            musicSource.Play();
        }
        if (scene == 6)
        {
            musicSource.clip = robotBattle;
            musicSource.Play();
        }
    }

    public void PlaySFXForScene(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    void SetClipsToSource()
    {
        sfxSource.clip = playerAttack;
        sfxSource.clip = playerBlock;
        sfxSource.clip = playerLand;
        sfxSource.clip = robotLaser;
        sfxSource.clip = robotMissile;
        sfxSource.clip = robotShock;
        sfxSource.clip = robotSwipe;
        sfxSource.clip = tomatoKick;
        sfxSource.clip = tomatoLand;
        sfxSource.clip = tomatoRoll;
        sfxSource.clip = tomatoThrow;
    }   
}
