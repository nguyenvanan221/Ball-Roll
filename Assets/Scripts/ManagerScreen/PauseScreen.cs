using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public static bool paused;

    public GameObject pauseScreen;

    void Start()
    {
        paused = false;

        
        //if (!Interstitial.Instance.showAds)
        //{
        //    SetPauseMenu(false);
        //}
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void SetPauseMenu(bool isPaused)
    {
        paused = isPaused;

        Time.timeScale = paused ? 0 : 1;
        pauseScreen.SetActive(paused);
    }

}
