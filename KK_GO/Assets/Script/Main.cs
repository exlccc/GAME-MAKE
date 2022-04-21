using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Main : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
   public void playgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Exitgame()
    {
        Application.Quit();
    }

    public void UIEnable()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;//Õ¦ÍßÂ³¶à£¡
    }

    public void ExitpauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }

    public void again_playgame()
    {
        SceneManager.LoadScene(1);
    }
}
