using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelManager : MonoBehaviour
{
    public event Action sceneStarted;
    public static GameLevelManager Instance;
    [SerializeField]
    GameDataManager gameDataManager;
    
    private static int firstLevelIndex = 1;
    private static int currentSceneIndex;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        gameDataManager.OnGameDataLoaded += Init;
    }
    private void Init()
    {
       sceneStarted?.Invoke();
    }
    public static void LoadFirstLvl()
    {
        SceneManager.LoadScene(firstLevelIndex);
    }
    public static void Loadlvl(int index)
    {
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// Try to load reached level is it exists
    /// </summary>
    /// <param name="levelReached"></param>
    public static void ContinueGame(int levelReached)
    {
         SceneManager.LoadScene(levelReached);
    }

    /// <summary>
    /// Loading next level should be used when level is finished
    /// </summary>
    public static void LoadNextLelev()
    {
      
        if (IsThereNextLevel())
            SceneManager.LoadScene(CurrentSceneIndex() + 1);
        else
        {
            Debug.Log("No more levels to continue. Back to main");
            SceneManager.LoadScene(CurrentSceneIndex());
        }
            
    }
    #region Checks
    /// <summary>
    /// Checking is there next scene
    /// </summary>
    /// <param name="indexToCheck"></param>
    /// <returns></returns>
    public static bool IsThereNextLevel()
    {
        return CurrentSceneIndex() < GetLastSceneIndex();
    }
    #endregion

    #region GetScenes
    public static int CurrentSceneIndex()=> SceneManager.GetActiveScene().buildIndex;
   
    public static int GetLastSceneIndex()=> SceneManager.sceneCountInBuildSettings - 1;   
    #endregion
}
