using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public HighScoreData highScoreData;

    //Encapsulation
    private string m_playerName;
    public string playerName{
        get{return m_playerName;}
         set{
            if(value.Length>0)
            {
                m_playerName=value;
            }
            else
            {
                m_playerName="Player";
            }

         }
    }


    private void Awake() {
        if(Instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        Instance=this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        LoadData();
    }

    [System.Serializable]
    public class HighScoreData
    {
        public string playerName;
        public int highScore;
    }

    public void LoadData()
    {
        string path=Application.persistentDataPath+"/userdata/highscore.json";
        
        if(File.Exists(path))
        {
            string json=File.ReadAllText(path);
            highScoreData=JsonUtility.FromJson<HighScoreData>(json);
        }
        else
        {
            highScoreData=new HighScoreData();
            highScoreData.playerName="Player";
            highScoreData.highScore=0;
        }
    }

    public void SaveData(int score)
    {
        string path = Application.persistentDataPath + "/UserData";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        path=Application.persistentDataPath+"/userdata/highscore.json";
        highScoreData.playerName=playerName;
        highScoreData.highScore=score;
        string json=JsonUtility.ToJson(highScoreData);
        File.WriteAllText(path, json);
    }
}
