using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        
        SceneManager.LoadScene(levelName);
    }
}
