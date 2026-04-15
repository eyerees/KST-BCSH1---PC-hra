using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public int width;
    public int height;
    public bool fullscreen;

    private const string ResXKey = "ResX";
    private const string ResYKey = "ResY";
    private const string FullscreenKey = "Fullscreen";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
        Apply();
    }

    public void SetResolution(int w, int h)
    {
        width = w;
        height = h;

        Apply();
        Save();
    }

    public void SetFullscreen(bool value)
    {
        fullscreen = value;

        Apply();
        Save();
    }

    public void Apply()
    {
        Screen.fullScreenMode = fullscreen
            ? FullScreenMode.FullScreenWindow
            : FullScreenMode.Windowed;

        Screen.SetResolution(width, height, fullscreen);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(ResXKey, width);
        PlayerPrefs.SetInt(ResYKey, height);
        PlayerPrefs.SetInt(FullscreenKey, fullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        width = PlayerPrefs.GetInt(ResXKey, Screen.currentResolution.width);
        height = PlayerPrefs.GetInt(ResYKey, Screen.currentResolution.height);
        fullscreen = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;
    }
}