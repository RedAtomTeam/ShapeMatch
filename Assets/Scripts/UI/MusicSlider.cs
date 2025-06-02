using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    private Slider _slider;
    private AudioService _audioService;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _slider.value = PlayerPrefs.GetFloat("SoundtracksVolume", 1f);
    }
    public void ChangeVolume(float value)
    {
        PlayerPrefs.SetFloat("SoundtracksVolume", value);
        if (_audioService == null)
        {
            _audioService = AudioService.Instance;
        }
        _audioService.ChangeVolumeSoundtracks(value);
    }
}
