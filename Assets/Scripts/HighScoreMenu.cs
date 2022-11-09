using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreMenu : MonoBehaviour
{
    [SerializeField] Text firstPlace;
    [SerializeField] Text secPlace;
    [SerializeField] Text thrdPlace;
    int bestScore;
    int secScore;
    int thrdScore;
    string bestPlayerText;
    string secPlayerText;
    string thrdPlayerText;
    void Start()
    {
        if (HighScoreSave.Instance != null)
        {
            bestPlayerText = HighScoreSave.Instance.bestPlayerName;
            bestScore = HighScoreSave.Instance.bestScore;
            secPlayerText = HighScoreSave.Instance.secPlayerName;
            secScore = HighScoreSave.Instance.secScore;
            thrdPlayerText = HighScoreSave.Instance.thrdPlayerName;
            thrdScore = HighScoreSave.Instance.thrdScore;
        }
        HighScoreShowRanking();
    }

   public void HighScoreShowRanking()
    {
        firstPlace.text = bestPlayerText + " : " + bestScore;
        secPlace.text = secPlayerText + " : " + secScore;
        thrdPlace.text = thrdPlayerText + " : " + thrdScore;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
