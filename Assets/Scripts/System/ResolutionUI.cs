using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettingsUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private List<Resolution> resolutions = new();

    private void Start()
    {
        BuildList();
        SetupDropdown();

        LoadFromManager();

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
    }

    private void BuildList()
    {
        var all = Screen.resolutions;

        foreach (var r in all)
        {
            if (!resolutions.Any(x => x.width == r.width && x.height == r.height))
            {
                resolutions.Add(r);
            }
        }
    }

    private void SetupDropdown()
    {
        List<string> options = new();

        for (int i = 0; i < resolutions.Count; i++)
        {
            options.Add($"{resolutions[i].width} x {resolutions[i].height}");
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
    }

    private void LoadFromManager()
    {
        var manager = SettingsManager.Instance;

        fullscreenToggle.isOn = manager.fullscreen;

        int index = resolutions.FindIndex(r =>
            r.width == manager.width && r.height == manager.height);

        resolutionDropdown.value = Mathf.Max(0, index);
    }

    private void OnResolutionChanged(int index)
    {
        var r = resolutions[index];
        SettingsManager.Instance.SetResolution(r.width, r.height);
    }

    private void OnFullscreenChanged(bool value)
    {
        SettingsManager.Instance.SetFullscreen(value);
    }
}