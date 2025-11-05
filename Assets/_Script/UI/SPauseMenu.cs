using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SPauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private InputSystem_Actions inputActions;
    private SAudioManager audioManager;
    private float originalVolume = 0.2f;
    private float reducedVolume = 0.025f;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        audioManager = FindFirstObjectByType<SAudioManager>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Pause.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        inputActions.Player.Pause.performed -= OnPausePerformed;
        inputActions.Disable();
    }

    private void Start()
    {
        Resume();
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (GameIsPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (audioManager != null && audioManager.IsPlaying("Testing"))
        {
            audioManager.SetVolume("Testing", originalVolume);
        }

        if (SGameInput.Instance != null)
        {
            SGameInput.Instance.EnableInput();
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        if (audioManager != null && audioManager.IsPlaying("Testing"))
        {
            audioManager.SetVolume("Testing", reducedVolume);
        }

        if (SGameInput.Instance != null)
        {
            SGameInput.Instance.DisableInput();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
