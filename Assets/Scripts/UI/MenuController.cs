using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            bool newState = !menuCanvas.activeSelf;

            menuCanvas.SetActive(newState);
            PauseController.SetPause(newState);
        }
    }
}