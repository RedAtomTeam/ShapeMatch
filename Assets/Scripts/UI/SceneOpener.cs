using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SceneOpener : MonoBehaviour
{
    [SerializeField] private bool _showAd = true;
    private string _sceneName;

    public void OpenScene(string sceneName)
    {
        if (YandexGame.timerShowAd > 60f && _showAd)
        {
            _sceneName = sceneName;
            YandexGame.FullscreenShow();
            YandexGame.CloseFullAdEvent += CloseSceneForCloseFullAdEvent;
        }
        else
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    public void CloseSceneForCloseFullAdEvent()
    {
        YandexGame.CloseFullAdEvent -= CloseSceneForCloseFullAdEvent;
        SceneManager.LoadSceneAsync(_sceneName);
    }
}
