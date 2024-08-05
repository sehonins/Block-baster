using UnityEngine;
using System;

public class BlockManager : MonoBehaviour
{
    public static Action AllBlockedDestroyedEvent;
    private int blockCount;

    private void Start()
    {
        foreach (var block in transform.GetComponentsInChildren<Block>())
        {
            blockCount++;
            //May need this one later
            //block.Init();
            block.OnBlockedDestroyedEvent += OnBlockDestroyed;
        }
    }

    //TODO: Delete before release
    #region For test purpose only
    private void Update()
    {
        KeyInput();
    }

    private void KeyInput()
    {
        if (!Application.isEditor) return;

        if (Input.GetKeyDown(KeyCode.N)) 
                GameLevelManager.LoadNextLelev();
    }
    #endregion
    private void OnBlockDestroyed(Block block)
    {
        blockCount --;
        if ( blockCount == 0)
        {
            block.OnBlockedDestroyedEvent -= OnBlockDestroyed;
            AllBlockedDestroyedEvent?.Invoke();
            GameLevelManager.LoadNextLelev();
        }
    }
}
