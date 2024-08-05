using System;
using UnityEngine;

public class InputControler : MonoBehaviour
{
    public Action ActionButtonPressed;
    public Action GamePaused;

    private int MainScreenIndex = 0;

    bool isGamePaused;
    public float XInput {  get; private set; }

    #region Singlton
    public static InputControler inputControler;
    private void Awake()
    {
        if (inputControler != null)
        {
            Destroy(this);
        }
    }
    #endregion

    private void Update()
    {
        XInput = Input.GetAxis("Horizontal");
        CheckButtonPressed();
    }
    private void CheckButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActionButtonPressed?.Invoke();
        }       

        if (Input.GetKeyDown(KeyCode.P))
        {
            OnGamePaused();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GameLevelManager.Loadlvl(MainScreenIndex);
        }
    }


    #region TODO: Move to another place
    private void OnGamePaused()
    {
        if (isGamePaused)
        {
            isGamePaused = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            isGamePaused = true;
            Time.timeScale = 0f;
        }
        GamePaused?.Invoke();
    }
    #endregion

}
