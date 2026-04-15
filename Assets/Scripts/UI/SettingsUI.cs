using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void OnEnable()
    {
        if (SoundEffectManager.Instance != null)
        {
            SoundEffectManager.Instance.SetSlider(volumeSlider);
        }
        
        if (returnToMenuButton != null)
        {
            bool isMainMenu = SceneManager.GetActiveScene().name == mainMenuSceneName;
            returnToMenuButton.gameObject.SetActive(!isMainMenu);
        }
    }

    private void Start()
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