using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioMixer mixer;

    [SerializeField] private AudioClip _mainMenuMusic;
    [SerializeField] private AudioClip _firstLevelMusic;
    [SerializeField] private AudioClip _secondLevelMusic;
    [SerializeField] private AudioClip _thirdLevelMusic;
    [SerializeField] private AudioClip _fourthLevelMusic;
    [SerializeField] private AudioClip _fifthLevelMusic;
    [SerializeField] private AudioClip _lobbyMusic;
 
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip _netClickSound;
    [SerializeField] private AudioClip _treeClickSound;
    [SerializeField] private AudioClip _pipeClickSound;

    [SerializeField] private AudioClip _scrollClickSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic(_mainMenuMusic);
                break;

            case "Level_1":
                PlayMusic(_firstLevelMusic);
                break;

            case "Level_2":
                PlayMusic(_secondLevelMusic);
                break;

            case "Level_3":
                PlayMusic(_thirdLevelMusic);
                break;

            case "Level_4":
                PlayMusic(_fourthLevelMusic);
                break;

            case "Level_5":
                PlayMusic(_fifthLevelMusic);
                break;

            case "Lobby":
                PlayMusic(_lobbyMusic);
                break;

            case "":
                PlayMusic(_firstLevelMusic);
                break;

            default:
                StopMusic();
                break;
        }
        BindAllButtons();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        musicSource.clip = null;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void PlayButtonClickSound()
    {
        PlaySFX(buttonClickSound);
        Debug.Log("PlayButtonClickSound");
        Debug.Log(buttonClickSound == null ? "нет" : "да");

    }

    public void PlayNetClickSound()
    {
        PlaySFX(_netClickSound);
    }

    public void PLayTreeSound()
    {
        PlaySFX(_treeClickSound);
    }

    public void PlayPipeSound()
    {
        PlaySFX(_pipeClickSound);
    }

    public void PlayScrollClickSound()
    {
        PlaySFX(_scrollClickSound);
    }

    private void BindAllButtons()
    {
        var roots = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (var root in roots)
        {
            Button[] buttons = root.GetComponentsInChildren<Button>(true);
            foreach (Button btn in buttons)
            {
                if (btn.GetComponent<ScrollButton>() != null)
                {
                    
                    btn.onClick.RemoveListener(PlayButtonClickSound);
                    continue;
                }
                btn.onClick.RemoveListener(PlayButtonClickSound);
                btn.onClick.AddListener(PlayButtonClickSound);
            }
        }
    }


}
