using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    [SerializeField] Button playerName;
    [SerializeField] InputField playerNameField;
    public string playerNameText;
    private string bestPlayerText;
    private int bestScore;
    public GameObject highScore;
    
    void Start()
    {
        if (HighScoreSave.Instance != null)
        {
            bestPlayerText = HighScoreSave.Instance.bestPlayerName;
            bestScore = HighScoreSave.Instance.bestScore;
        }
        
        HighScoreText();
    }

   

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exitgame()
    {
        HighScoreSave.Instance.SaveHighscore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void HighScoreMenu()
    {
        SceneManager.LoadScene(2);
    }


    public void SetPlayerName()
    {
        playerNameText = playerNameField.text;
        Debug.Log(playerNameText);
        HighScoreSave.Instance.playerName = playerNameText;
    }

    public void HighScoreText()
    {
        string bestScorestring = bestScore.ToString();
        highScore.GetComponent<Text>().text = bestPlayerText + " : " + bestScore;
    }


    public void ResetHighscore()
    {
        HighScoreSave.Instance.bestScore = 0;
        HighScoreSave.Instance.secScore = 0;
        HighScoreSave.Instance.thrdScore = 0;
        HighScoreSave.Instance.bestPlayerName = "No Player1";
        HighScoreSave.Instance.secPlayerName = "No Player2";
        HighScoreSave.Instance.thrdPlayerName = "No Player3";
        SceneManager.LoadScene(0);
    }
}
