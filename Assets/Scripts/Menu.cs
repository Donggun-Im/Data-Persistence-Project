using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public InputField nameInput;
    public static Menu Instance;
    public string nickName;
    public int bestScore;
    public string tempName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        Menu.Instance.LoadBestScore();
        bestScoreText.text = "Best Score : " + Menu.Instance.nickName + " : " + Menu.Instance.bestScore;
    }

    [System.Serializable]
    class UserData
    {
        public string nickName;
        public int bestScore; 
    }

    

    public void SaveBestScore()
    {
        UserData data = new UserData();
        data.nickName = nickName;
        data.bestScore = bestScore;
        string Json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", Json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string Json = File.ReadAllText(path);
            UserData data = JsonUtility.FromJson<UserData>(Json);
            nickName = data.nickName;
            bestScore = data.bestScore;
            Debug.Log("Load");
        }
        
    }

    public void SaveName()
    {
        Menu.Instance.tempName = nameInput.text;
    }

    public void StartGame()
    {
        if(nameInput.text.Length > 0)
        {
            SaveName();
            SceneManager.LoadScene(1);
        }
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
