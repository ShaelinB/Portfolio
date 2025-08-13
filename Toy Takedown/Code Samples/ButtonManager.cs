using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    public bool hasPauseScreen;
    public GameObject pauseScene;
    public List<GameObject> objectsRendered;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        
        
        
            LoadVolume();
        /*
        else
        {
            SetMaster();
            SetMusic();
            SetSFX();
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPauseScreen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                ShowScene(pauseScene);
                for (int i = 0; i < objectsRendered.Count; i++)
                {
                    deRenderObjects(objectsRendered[i]);
                }

            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowScene(GameObject screen)
    {
        screen.SetActive(true);
    }

    public void HideScene(GameObject screen)
    {
        screen.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetMaster()
    {
        float volume = masterSlider.value;
        mixer.SetFloat("Master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MasterSlider", volume);
    }

    public void SetMusic()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicSlider", volume);
    }

    public void SetSFX()
    {
        float volume = sfxSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXSlider", volume);
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterSlider",50);
        musicSlider.value = PlayerPrefs.GetFloat("MusicSlider",50);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXSlider",50);

        SetMaster();
        SetMusic();
        SetSFX();
    }

    public void deRenderObjects(GameObject obj)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    public void RenderObjects(GameObject obj)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        renderer.enabled = true;
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        AudioSource sfx = audioManager.sfxSource;
        AudioSource music = audioManager.musicSource;
        AudioMixerGroup[] audioMixerGroup = mixer.FindMatchingGroups("Master");
        music.outputAudioMixerGroup = audioMixerGroup[1];
        sfx.outputAudioMixerGroup = audioMixerGroup[2];
    }
}
