using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    [Header("General settings")]
    [SerializeField] private GameObject settingsPanel;

    [Header("Main menu")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject quitGamePanel;

    [Header("Game scene")]
    [SerializeField] private GameObject gamePausePanel;
    [SerializeField] private GameObject pausePanel;
    
    private VolumeSettings volumeSettings;

    private bool paused = false;
    private PlayerInput playerInput;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {
            playerInput = GameObject.FindObjectOfType<PlayerInput>();
            volumeSettings = GameObject.FindAnyObjectByType<VolumeSettings>();
            Time.timeScale = 1;
            paused = false;
            volumeSettings.PlayGameMusic();
            gamePausePanel.SetActive(false);
            pausePanel.SetActive(false);
            settingsPanel.SetActive(false);

        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenuPanel.SetActive(true);
            settingsPanel.SetActive(false);
            quitGamePanel.SetActive(false);
        }
    }

    public void PauseGameButton()
    {
        if (paused)
        {
            volumeSettings.PlayGameMusic();
            Time.timeScale = 1;
            gamePausePanel.SetActive(false);
            pausePanel.SetActive(false);
            settingsPanel.SetActive(false);
        }
        else
        {
            volumeSettings.PauseGameMusic();
            Time.timeScale = 0;
            gamePausePanel.SetActive(true);
            pausePanel.SetActive(true);
            settingsPanel.SetActive(false);
        }
        paused = !paused;
    }

    public void Continue()
    {
        paused = false;
        playerInput.isPauseGame = false;
        volumeSettings.PlayGameMusic();
        Time.timeScale = 1;
        gamePausePanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void SettingsPanel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenuPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            pausePanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
    }

    public void BackToPausePanel()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            settingsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
            if(quitGamePanel !=null)
            {
                quitGamePanel.SetActive(false);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitGamePanel()
    {
        mainMenuPanel.SetActive(false);
        quitGamePanel.SetActive(true);
    }

}
