using UnityEngine;
using YG;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private ActionBar _actionBar;


    private void Start()
    {
        _actionBar.winEvent += SaveWinLevel;
    }

    public void SaveWinLevel()
    {
        string currentScene = gameObject.scene.name;
        foreach (var level in YandexGame.savesData.levels)
        {
            if (level.sceneName == currentScene)
            {
                level.status = 1;
                YandexGame.SaveProgress();
            }
        }
    }
}
