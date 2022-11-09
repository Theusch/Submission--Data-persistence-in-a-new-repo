using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;

    private int m_Points;
    private int bestScore;
    private int secScore;
    
    private string m_PLayerName;
    private string bestPlayerName;
    private string secPlayerName;
    
    private bool m_GameOver = false;
    

    
    // Start is called before the first frame update
    void Start()
    {
        if (HighScoreSave.Instance != null)
        {
            bestScore = HighScoreSave.Instance.bestScore;
            secScore = HighScoreSave.Instance.secScore;
            m_PLayerName = HighScoreSave.Instance.playerName;
            bestPlayerName = HighScoreSave.Instance.bestPlayerName;
            secPlayerName = HighScoreSave.Instance.secPlayerName;
        }
       
        
        BestScoreText();
        Debug.Log(m_PLayerName);
        SpawnBricks();
    }

    private void Update()
    {
        ReleaseTheBall();
    }

    void ReleaseTheBall()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void SpawnBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";       
        
    }

    public void GameOver()
    {
        m_GameOver = true;
        BestScore();        
        GameOverText.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void BestScore()
    {
        if (m_Points > HighScoreSave.Instance.bestScore)   // Top Highscore  
        {
            HighScoreSave.Instance.secScore = bestScore;            //1.Place -> 2.Place
            HighScoreSave.Instance.secPlayerName = bestPlayerName;
            HighScoreSave.Instance.thrdScore = secScore;            //2.Place -> 3.Place
            HighScoreSave.Instance.thrdPlayerName = secPlayerName;
            HighScoreSave.Instance.bestScore = m_Points;            //New Highscore
            bestScore = HighScoreSave.Instance.bestScore;           
            secScore = HighScoreSave.Instance.secScore;
            HighScoreSave.Instance.bestPlayerName = m_PLayerName;
            
        }
        else if (m_Points > HighScoreSave.Instance.secScore)
        {
            HighScoreSave.Instance.thrdScore = secScore;            //2.Place -> 3.Place
            HighScoreSave.Instance.thrdPlayerName = secPlayerName;
            HighScoreSave.Instance.secScore = m_Points;
            HighScoreSave.Instance.secPlayerName = m_PLayerName;
        }
        else if (m_Points > HighScoreSave.Instance.thrdScore)
        {
            HighScoreSave.Instance.thrdScore = m_Points;
            HighScoreSave.Instance.thrdPlayerName = m_PLayerName;
        }
    }

    public void BestScoreText()
    {
        bestScoreText.text = bestPlayerName + " : " + bestScore;
    }
}
