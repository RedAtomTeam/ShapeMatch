using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private ActionBar _actionBar;
    [SerializeField] private LevelsConfig _levelsConfig;


    private void Start()
    {
        _actionBar.winEvent += SaveWinLevel;
    }

    public void SaveWinLevel()
    {
        string currentScene = gameObject.scene.name;

        foreach (var level in _levelsConfig.levels)
            if (level.sceneName == currentScene)
                level.status = 1;
    }
}
