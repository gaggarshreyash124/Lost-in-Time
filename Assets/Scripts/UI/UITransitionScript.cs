using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITransitionScript : MonoBehaviour
{
    public CinemachineCamera CurrentCam;
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Start()
    {

    }

    public void SwitchToCam(CinemachineCamera target)
    {
        CurrentCam.Priority--;
        CurrentCam = target;
        CurrentCam.Priority++;
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }
    public void Unpause(GameObject pauseMenu)
    {
        Debug.Log("Unpausing");
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    void Update()
    {
        if (pauseMenu != null)
        {
            if (PlayerInputHandler.Instance.PauseInput && !isPaused)
            {

                Pause(pauseMenu);
                isPaused = true;
            }
        }


    }

    private void Pause(GameObject pauseMenu)
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
}
