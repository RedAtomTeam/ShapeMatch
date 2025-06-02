using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelField_Switch : MonoBehaviour
{
    [SerializeField] private Button _switchTabLeftButton;
    [SerializeField] private Button _switchTabRightButton;

    [SerializeField] private List<RectTransform> _tabs;
    [SerializeField] private int _selectedTabIndex;


    private void OnEnable()
    {
        DefaultPos();

        _switchTabLeftButton.onClick.AddListener(SwitchTabLeft);
        _switchTabRightButton.onClick.AddListener(SwitchTabRight);

        UpdateSwitchButtons();

    }

    private void OnDisable()
    {
        _switchTabLeftButton.onClick.RemoveListener(SwitchTabLeft);
        _switchTabRightButton.onClick.RemoveListener(SwitchTabRight);
    }

    private void DefaultPos()
    {
        _selectedTabIndex = 0;
        for (int i = 0; i < _tabs.Count; i++)
        {
            Vector3 newPos = _tabs[i].anchoredPosition;
            newPos.x = _tabs[i].rect.width * i;
            _tabs[i].anchoredPosition = newPos;

            //print(_tabs[i].rect.width);
            //print(i);
            //print(newPos);
        }
    }

    private void SwitchTabLeft()
    {
        foreach (var tab in _tabs)
            tab.position += new Vector3(tab.rect.width, 0, 0);

        _selectedTabIndex--;
        UpdateSwitchButtons();
    }

    private void SwitchTabRight()
    {
        foreach (var tab in _tabs)
            tab.position -= new Vector3(tab.rect.width, 0, 0);

        _selectedTabIndex++;
        UpdateSwitchButtons();
    }

    private void UpdateSwitchButtons()
    {
        _switchTabLeftButton.gameObject.SetActive(_selectedTabIndex != 0);
        _switchTabRightButton.gameObject.SetActive(_selectedTabIndex != _tabs.Count - 1);
    }

}
