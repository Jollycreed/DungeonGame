using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;

public class OptionsController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject optionsMenu;           // Reference to the Options Menu Canvas
    public Slider volumeSlider;              // Reference to the Volume Slider
    public Toggle fullscreenToggle;          // Reference to the Fullscreen Toggle
    public Dropdown resolutionDropdown;      // Reference to the Resolution Dropdown
    public AudioMixer audioMixer;            // Reference to the AudioMixer

    private List<Vector2Int> customResolutions = new List<Vector2Int> // Predefined resolution list
    {
        new Vector2Int(1920, 1080), // Full HD
        new Vector2Int(1600, 900),  // HD+
        new Vector2Int(1366, 768),  // HD
        new Vector2Int(1280, 720),  // 720p
        new Vector2Int(800, 600),   // SVGA
        new Vector2Int(640, 480)    // VGA
    };

    private const string VolumeKey = "MasterVolume";
    private const string FullscreenKey = "Fullscreen";
    private const string ResolutionIndexKey = "ResolutionIndex";

    void Start()
    {
        LoadSettings();
        SetupResolutionDropdown();
        SetupVolumeSlider();
        SetupFullscreenToggle();
    }

    // ------------------ Volume Methods ------------------

    private void SetupVolumeSlider()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.75f);
        volumeSlider.value = savedVolume;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(savedVolume) * 20);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save(); // Save immediately
    }

    // ------------------ Fullscreen Methods ------------------

    private void SetupFullscreenToggle()
    {
        bool isFullscreen = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(FullscreenKey, isFullscreen ? 1 : 0);
        PlayerPrefs.Save(); // Save immediately
    }

    // ------------------ Resolution Methods ------------------

    private void SetupResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < customResolutions.Count; i++)
        {
            string option = customResolutions[i].x + " x " + customResolutions[i].y;
            options.Add(option);

            if (customResolutions[i].x == Screen.currentResolution.width &&
                customResolutions[i].y == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt(ResolutionIndexKey, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetResolution(int resolutionIndex)
    {
        Vector2Int resolution = customResolutions[resolutionIndex];
        Screen.SetResolution(resolution.x, resolution.y, Screen.fullScreen);
        PlayerPrefs.SetInt(ResolutionIndexKey, resolutionIndex);
        PlayerPrefs.Save(); // Save immediately
    }

    // ------------------ Load Settings ------------------

    private void LoadSettings()
    {
        // Load resolution
        if (PlayerPrefs.HasKey(ResolutionIndexKey))
        {
            int resolutionIndex = PlayerPrefs.GetInt(ResolutionIndexKey);
            resolutionDropdown.value = resolutionIndex;
            resolutionDropdown.RefreshShownValue();
            SetResolution(resolutionIndex); // Apply resolution
        }

        // Load volume
        if (PlayerPrefs.HasKey(VolumeKey))
        {
            float volume = PlayerPrefs.GetFloat(VolumeKey);
            volumeSlider.value = volume;
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }

        // Load fullscreen
        bool isFullscreen = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
}
