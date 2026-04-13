using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    // The public Instance allows other scripts to call SoundEffectManager.Instance
    public static SoundEffectManager Instance { get; private set; }

    private AudioSource audioSource;
    private AudioSource randomPitchAudioSource;
    private AudioSource voiceAudioSource;
    private SoundEffectLibrary soundEffectLibrary;
    
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Keeps the manager alive when changing scenes
            DontDestroyOnLoad(gameObject); 

            AudioSource[] audioSources = GetComponents<AudioSource>();
            audioSource = audioSources[0];
            randomPitchAudioSource = audioSources[1];
            voiceAudioSource = audioSources[2];
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Load the previously saved volume or default to 0.5f
        float savedVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        if (sfxSlider != null)
        {
            sfxSlider.value = savedVolume;
            sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
        }

        SetVolume(savedVolume);
    }

    public static void Play(string soundName, bool randomPitch = false)
    {
        if (Instance == null || Instance.soundEffectLibrary == null) return;

        AudioClip audioClip = Instance.soundEffectLibrary.GetRandomClip(soundName);
        if (audioClip != null)
        {
            if (randomPitch)
            {
                Instance.randomPitchAudioSource.pitch = Random.Range(1f, 1.5f);
                Instance.randomPitchAudioSource.PlayOneShot(audioClip);
            }
            else
            {
                Instance.audioSource.PlayOneShot(audioClip);
            }
        }
    }

    public static void PlayVoice(AudioClip audioClip, float pitch = 1f)
    {
        if (Instance == null) return;
        Instance.voiceAudioSource.pitch = pitch;
        Instance.voiceAudioSource.PlayOneShot(audioClip);
    }

    public static void SetVolume(float volume)
    {
        if (Instance == null) return;
        Instance.audioSource.volume = volume;
        Instance.randomPitchAudioSource.volume = volume;
        Instance.voiceAudioSource.volume = volume;
        
        // Save the volume setting so it persists across game restarts
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSlider(Slider newSlider)
    {
        sfxSlider = newSlider;
        if (sfxSlider != null)
        {
            sfxSlider.value = audioSource.volume; 
            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
        }
    }

    public void OnValueChanged()
    {
        if (sfxSlider != null)
        {
            SetVolume(sfxSlider.value);
        }
    }
}