using UnityEngine;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
    public GameObject optionsScreen;
    public GameObject keybindScreen;
    public Button backToGameBtn;
    public Button backToOptionsBtn;
    public Button toKeybindBtn;
    public Button muteButton;
    public GameObject overlay;

    private bool isMuted = false;
    public AudioSource currentAudioSource;

    private bool isPaused = false; 

    void Start()
    {
        optionsScreen.SetActive(false);
        backToGameBtn.onClick.AddListener(ResumeGame);
        backToOptionsBtn.onClick.AddListener(BackToOptions);
        toKeybindBtn.onClick.AddListener(OpenKeybinds);
        muteButton.onClick.AddListener(ToggleMute);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)  
            {
                ResumeGame();
            }
            else  
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;  
        optionsScreen.SetActive(true);
        Time.timeScale = 0f;
        overlay.SetActive(false);
    }

    void ResumeGame()
    {
        isPaused = false; 
        optionsScreen.SetActive(false);
        keybindScreen.SetActive(false);
        Time.timeScale = 1f;
        overlay.SetActive(true);
    }

    void OpenKeybinds()
    {
        optionsScreen.SetActive(false);
        keybindScreen.SetActive(true);
    }

    void BackToOptions()
    {
        optionsScreen.SetActive(true);
        keybindScreen.SetActive(false);
    }

    void ToggleMute()
    {
        isMuted = !isMuted;
        currentAudioSource.mute = isMuted;
    }
}



