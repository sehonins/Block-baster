using TMPro;
using UnityEngine;

public class SceneInfoDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI sceneInfo;

    private void Start()
    {
        sceneInfo.text = $"{GameLevelManager.CurrentSceneIndex()}/{GameLevelManager.GetLastSceneIndex()}";
    }
}
