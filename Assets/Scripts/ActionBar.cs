using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private RectTransform _actionBarField;
    [SerializeField] private ActionBar_Element _elementPrefab;
    [Range(5, 10)]
    [SerializeField] private int _elementsCount;
    [Range(2, 5)]
    [SerializeField] private int _elementsLineLengthToParse;

    [SerializeField] private Color _disactiveElementColor;
    [SerializeField] private Shape _disactiveElementShape;

    private List<ActionBar_Element> _actionBarElements;

    private int _firstEmptyIndex = 0;

    public bool IsEmpty
    {
        get {
            bool empty = true;

            foreach (var element in _actionBarElements)
            {
                empty &= element.IsEmpty;
            }
            return empty;
        }
    }

    public event Action winEvent;
    public event Action looseEvent;
    private event Action addElement;
    



    private void Start()
    {
        addElement += CheckAfterAdd;
        _actionBarElements = new List<ActionBar_Element>(_elementsCount);
        CreateAndDistributeElements();
    }

    private void CreateAndDistributeElements()
    {
        foreach (var element in _actionBarElements)
        {
            if (element != null)
                Destroy(element.gameObject);
        }
        _actionBarElements.Clear();

        float containerWidth = _actionBarField.rect.width;
        float elementWidth = _elementPrefab.GetComponent<RectTransform>().rect.width;

        float totalElementsWidth = _elementsCount * elementWidth;
        float spacing = (containerWidth - totalElementsWidth) / (_elementsCount + 1);

        for (int i = 0; i < _elementsCount; i++)
        {
            ActionBar_Element newElement = Instantiate(_elementPrefab, _actionBarField);
            RectTransform elementRT = newElement.GetComponent<RectTransform>();

            float xPos = -containerWidth / 2 + spacing * (i + 1) + elementWidth * (i + 0.5f);
            elementRT.anchoredPosition = new Vector2(xPos, 0);

            _actionBarElements.Add(newElement);
        }

        for (int i = 0; i < _elementsCount; i++)
            RemoveAt(i);
    }

    private void ParseAndRemoveSame()
    {
        for (int i = 0; i < _elementsCount - _elementsLineLengthToParse + 1; i++)
        {
            bool isAllSame = true;
            ActionBar_Element element = _actionBarElements[i];
            for (int j = i; j < i + _elementsLineLengthToParse; j++)
            {
                if (!_actionBarElements[j].Equals(element))
                {
                    isAllSame = false;
                    break;
                }
            }

            if (isAllSame)
            {
                for (int j = i; j < i + _elementsLineLengthToParse; j++)
                {
                    RemoveAt(j);
                }
                i = 0;
            }
        }
        ResetFirstEmptyIndex();
    }

    private void ResetFirstEmptyIndex()
    {
        for (int i = 0; i < _elementsCount; i++)
        {
            if (_actionBarElements[i].IsEmpty)
            {
                _firstEmptyIndex = i;
                break;
            }
        }
    }
    

    public bool TryAdd(Piece piece)
    {
        if (HasEmptyCell())
        {
            _actionBarElements[_firstEmptyIndex].Put(piece);
            _firstEmptyIndex += 1;
            addElement?.Invoke();
            return true;
        }
        return false;
    }

    private void CheckAfterAdd()
    {
        ParseAndRemoveSame();
        if (_firstEmptyIndex >= _elementsCount)
            Loose();
    }

    private void Loose()
    {
        looseEvent?.Invoke();
    }

    private bool HasEmptyCell()
    {
        if (_firstEmptyIndex < _elementsCount)
            return true;
        return false;
    }

    public void RemoveAt(int index)
    {
        _actionBarElements[index].Pop(_disactiveElementColor, _disactiveElementShape);
    } 
}
