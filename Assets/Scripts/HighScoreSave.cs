using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreSave : MonoBehaviour
{
    public static HighScoreSave Instance;
    
    public int bestScore;
    public int secScore;
    public int thrdScore;

    public string playerName;
    public string bestPlayerName;
    public string secPlayerName;
    public string thrdPlayerName;


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
        public int secScore;
        public int thrdScore;

        public string bestPlayerName;
        public string secPlayerName;
        public string thrdPlayerName;
    }

    public void SaveHighscore()
    {
        SaveData data = new SaveData();
        data.bestPlayerName = bestPlayerName;
        data.bestScore = bestScore;
        data.secPlayerName = secPlayerName;
        data.secScore = secScore;
        data.thrdPlayerName = thrdPlayerName;
        data.thrdScore = thrdScore;

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
            secPlayerName = data.secPlayerName;
            secScore = data.secScore;
            thrdPlayerName = data.thrdPlayerName;
            thrdScore = data.thrdScore;
            
        }
        
    }
}
