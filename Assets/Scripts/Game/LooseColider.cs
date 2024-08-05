using System.Collections;
using UnityEngine;

public class LooseColider : MonoBehaviour
{
    private float LoseMenuTimeInSecunds = 1f;
    private int MainScreenIndex = 0;
    private void OnTriggerEnter2D(Collider2D collision) => OnBallFeltDown();
    
    public void OnBallFeltDown()
    {
        ShowGameResult();
        StartCoroutine(LostGame());
    }
    private IEnumerator LostGame()
    {
        yield return new WaitForSeconds(LoseMenuTimeInSecunds);
        GameLevelManager.Loadlvl(MainScreenIndex);
    }

    private void ShowGameResult()
    {
        Debug.Log("You have lost");
    }
}
