using UnityEngine;
using UnityEngine.UI;

public class EffectsSlider : MonoBehaviour
{
    private Slider _slider;
    private AudioService _audioService;

    private void Awake()
    {
        _slider = GetComponent<Slider>();   
    }

    private void OnEnable()
    {
        _slider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", 1f);
    }

    public void ChangeVolume(float value)
    {
        PlayerPrefs.SetFloat("SoundEffectsVolume", value);
        if (_audioService == null)
        {
            _audioService = AudioService.Instance;
        }
        _audioService.ChangeVolumeSoundEffects(value);
    }
}
