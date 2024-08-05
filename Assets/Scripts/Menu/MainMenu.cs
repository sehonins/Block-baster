using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameDataManager dataManager;

    public void OnNewGameStart()
    {
       GameLevelManager.LoadFirstLvl();
    }

    public void OnGameContinue()
    {      
        GameLevelManager.ContinueGame(dataManager.LevelReached);
    }
}
