using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelField_Switch : MonoBehaviour
{
    [SerializeField] private Button _switchTabLeftButton;
    [SerializeField] private Button _switchTabRightButton;

    [SerializeField] private List<RectTransform> _tabs;
    [SerializeField] private List<Vector3> _tabsPositions;
    [SerializeField] private int _selectedTabIndex;

    [SerializeField] private float _changeTabTime;

    private void Awake()
    {
        for (int i = 0; i < _tabs.Count; i++)
        {
            _tabsPositions.Add(_tabs[i].anchoredPosition);
            print(_tabs[i].anchoredPosition);
        }
    }

    private void OnEnable()
    {
        _switchTabLeftButton.onClick.AddListener(SwitchTabLeft);
        _switchTabRightButton.onClick.AddListener(SwitchTabRight);
        UpdateSwitchButtons();
    }

    private void OnDisable()
    {
        _switchTabLeftButton.onClick.RemoveListener(SwitchTabLeft);
        _switchTabRightButton.onClick.RemoveListener(SwitchTabRight);
    }

    public void DefaultPos()
    {
        _selectedTabIndex = 0;
        for (int i = 0; i < _tabs.Count; i++)
        {
            _tabs[i].anchoredPosition = new Vector3(_tabsPositions[i].x, 0, 0);
            print(_tabs[i].anchoredPosition);
        }
    }

    private void SwitchTabLeft()
    {
        _switchTabLeftButton.onClick.RemoveListener(SwitchTabLeft);
        Tween tween = null;
        for (int i = 0; i < _tabs.Count; i++)
        {
            tween = _tabs[i].DOAnchorPos(_tabsPositions[i - _selectedTabIndex <= -1 ? _selectedTabIndex - i - 1 : i - _selectedTabIndex + 1], _changeTabTime);
        }                                               
        if (tween is not null)
            tween.OnComplete(() => { _switchTabLeftButton.onClick.AddListener(SwitchTabLeft); });
        _selectedTabIndex--;
        UpdateSwitchButtons();
    }

    private void SwitchTabRight()
    {
        _switchTabRightButton.onClick.RemoveListener(SwitchTabRight);
        Tween tween = null;
        for (int i = 0; i < _tabs.Count; i++)
        {
            tween = _tabs[i].DOAnchorPos(_tabsPositions[i - _selectedTabIndex <= 0 ? _tabsPositions.Count - 1 - _selectedTabIndex : i - _selectedTabIndex - 1], _changeTabTime);
        }
        if (tween is not null)
            tween.OnComplete(() => { _switchTabRightButton.onClick.AddListener(SwitchTabRight);});
        _selectedTabIndex++;
        UpdateSwitchButtons();
    }

    private void UpdateSwitchButtons()
    {
        _switchTabLeftButton.gameObject.SetActive(_selectedTabIndex != 0);
        _switchTabRightButton.gameObject.SetActive(_selectedTabIndex != _tabs.Count - 1);
    }
}
