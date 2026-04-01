using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITransitionScript : MonoBehaviour
{
   public CinemachineCamera CurrentCam;

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
        SceneManager.LoadScene(0);
    }
    public void Unpause(GameObject pauseMenu)
     {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
     

}
