using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreSave : MonoBehaviour
{
    public static HighScoreSave Instance;
    public int score;
    public int bestScore;
    public string playerName;
    public string bestPlayerName;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }


    [System.Serializable]

    class SaveData
    {
        public int bestScore;
        public string bestPlayerName;
    }

    public void SaveHighscore()
    {
        SaveData data = new SaveData();
        data.bestPlayerName = bestPlayerName;
        data.bestScore = bestScore;

        string Json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath +"/Submissionsave.json", Json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/Submissionsave.json";
        if (File.Exists(path))
        {
            string Json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(Json);

            bestPlayerName = data.bestPlayerName;
            bestScore = data.bestScore;
            
        }
        
    }
}
