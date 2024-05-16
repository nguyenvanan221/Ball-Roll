using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Score bestScore;
    public Score lastScore;

    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI lastScoreText;

    private void Awake()
    {
        LoadScoreSO();
    }

    public void LoadLevel(string levelName)
    {
        
        SceneManager.LoadScene(levelName);
    }

    //load best score and last score from SO
    public void LoadScoreSO()
    {
        bestScoreText.SetText(string.Format("{0:0}", bestScore.value)); 
        lastScoreText.SetText(string.Format("{0:0}", lastScore.value)); 
    }
}
