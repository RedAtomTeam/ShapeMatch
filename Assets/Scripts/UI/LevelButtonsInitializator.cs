using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonsInitializator : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    [SerializeField] private LevelsConfig _levelsConfig;
    [SerializeField] private int _levelId;

    [SerializeField] private Color _openLevelColor;
    [SerializeField] private Color _closeLevelColor;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (CheckLevelStatus())
        {
            _button.onClick.AddListener(OpenLevel);
            _image.color = _openLevelColor;
        }
        else
        {
            _image.color = _closeLevelColor;
        }
    }

    private void OnDisable()
    {
        if (CheckLevelStatus())
        {
            _button.onClick.RemoveListener(OpenLevel);
        }
    }

    private void OpenLevel()
    {
        string levelName = "";

        foreach (var level in _levelsConfig.levels)
            if (level.id == _levelId) 
                levelName = level.sceneName;
        print(levelName);
        SceneManager.LoadSceneAsync(levelName);
    }

    private bool CheckLevelStatus()
    {
        if (_levelId == 0)
            return true;

        foreach (var level in _levelsConfig.levels)
        {
            if (level.id == _levelId - 1)
                return level.status == 1;
            if (level.id == _levelId)
                return level.status == 1;
        }
        return true;
    }
}
