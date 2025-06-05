using DG.Tweening;
using UnityEngine;


public class TabSwitcher : MonoBehaviour
{
    [SerializeField] private RectTransform _firstTabRectTransform;
    [SerializeField] private RectTransform _secondTabRectTransform;

    [SerializeField] private float _time;

    public void SwitchTabs()
    {
        var firstPos = _firstTabRectTransform.position;
        var secondPos = _secondTabRectTransform.position;

        _firstTabRectTransform.DOMove(secondPos, _time).OnComplete(() => _firstTabRectTransform.gameObject.SetActive(false));
        _secondTabRectTransform.DOMove(firstPos, _time);
    }

}
