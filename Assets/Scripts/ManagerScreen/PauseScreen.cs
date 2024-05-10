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

    }

    public void Restart()
    {
        //Time.timeScale = 1.0f;
        //RewardedAd.Instance.LoadAd();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(WaitRestart());
    }

    public void MainMenu()
    {
        //Time.timeScale = 1.0f;
        //RewardedAd.Instance.LoadAd();
        //SceneManager.LoadScene("MainMenu");
        StartCoroutine(WaitMainMenu());

    }

    IEnumerator WaitMainMenu()
    {
        Time.timeScale = 1.0f;
        RewardedAd.Instance.LoadAd();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator WaitRestart()
    {
        Time.timeScale = 1.0f;
        RewardedAd.Instance.LoadAd();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetPauseMenu(bool isPaused)
    {
        paused = isPaused;

        Time.timeScale = paused ? 0 : 1;
        pauseScreen.SetActive(paused);
    }

}
