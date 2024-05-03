using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        if (Interstitial.Instance.showAds)
        {
            Interstitial.Instance.ShowAd();
        }

        SceneManager.LoadScene(levelName);
    }
}
