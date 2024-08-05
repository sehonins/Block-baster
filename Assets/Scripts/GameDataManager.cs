using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public event Action OnGameDataLoaded;
    
    public GameData GameData {  get; private set; }
    public int LevelReached { get; private set; }   

    const string ProgressKey = "LevelReached";
    const string GameDataFilePath = "C:\\GameDev\"GameData.json";

    private void Awake()
    {
        BlockManager.AllBlockedDestroyedEvent += SaveProgress;
        GameData = new GameData();
    }
    private void Start()
    {
        LoadProgress();
    }

    private void SaveProgress()
    {
        if (GameLevelManager.IsThereNextLevel())
            LevelReached = GameLevelManager.CurrentSceneIndex() + 1;
        else
            LevelReached = GameLevelManager.CurrentSceneIndex();
        PlayerPrefs.SetInt(ProgressKey, LevelReached);
    }
    private void LoadProgress()
    {
        LevelReached = PlayerPrefs.GetInt(ProgressKey);
        Debug.Log("Loading");
        OnGameDataLoaded?.Invoke();
    }
    #region Game data
    private void SaveGameData()
    {
        if (File.Exists(GameDataFilePath))
        {
            GameData gameData = new GameData();
            gameData.ReachedLevel = GameLevelManager.CurrentSceneIndex() + 1;
            string json = JsonUtility.ToJson(gameData);
            File.WriteAllText(GameDataFilePath, json);
        }
        else
        {
            GameData = new GameData();
            GameData.ReachedLevel = GameLevelManager.CurrentSceneIndex() + 1;
            string json = JsonUtility.ToJson(GameData);
            File.WriteAllText(GameDataFilePath, json);
        }
       
        
    }

    private IEnumerator LoadGameData()
    {
        if (File.Exists(GameDataFilePath))
        {
            string json = File.ReadAllText(GameDataFilePath);
            yield return null;
            GameData gameData = JsonUtility.FromJson<GameData>(json);
            OnGameDataLoaded?.Invoke();

        }
        else
        {
            Debug.LogWarning("Save file not found");
        }        
    }
    #endregion
    private void OnDisable()
    {
        BlockManager.AllBlockedDestroyedEvent -= SaveGameData;
    }
}
