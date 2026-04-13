using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene switching

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    void OnEnable()
    {
        if (SoundEffectManager.Instance != null)
        {
            SoundEffectManager.Instance.SetSlider(volumeSlider);
        }

        if (returnToMenuButton != null)
        {
            bool isIngame = SceneManager.GetActiveScene().name != mainMenuSceneName;
            returnToMenuButton.gameObject.SetActive(isIngame);
        }
    }

    void Start()
    {
        if (returnToMenuButton != null)
        {
            returnToMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    private void ReturnToMainMenu()
    {
        PauseController.ResetPause();
        SceneManager.LoadScene(mainMenuSceneName);
    }
}