using UnityEngine;
using UnityEngine.UI;

public class ActionBar_Element : MonoBehaviour
{

    [SerializeField] private Image _iconRenderer;
    [SerializeField] private Image _shapeRenderer;
    [SerializeField] private Image _outsideOutlineRenderer;
    [SerializeField] private Image _insideOutlineRenderer;
    
    private bool _isEmpty = true;
    public bool IsEmpty { get => _isEmpty; private set => _isEmpty = value; }


    public void Put(Piece piece)
    {
        _iconRenderer.color = new Color(1, 1, 1, 1);
        _iconRenderer.sprite = piece.IconSprite;
        _shapeRenderer.color = piece.InsideColor;
        _shapeRenderer.sprite = piece.ShapeSprite;
        _insideOutlineRenderer.color = piece.InsideOutlineColor;
        _insideOutlineRenderer.sprite = piece.InsideOutlineSprite;
        _outsideOutlineRenderer.color = piece.OutsideOutlineColor;
        _outsideOutlineRenderer.sprite = piece.OutsideOutlineSprite;

        IsEmpty = false;
    }

    public void Pop(Color insideColor, Color insideOutlineColor, Color outsideOutlineColor, Shape shape) 
    {
        _iconRenderer.color = new Color(0, 0, 0, 0);
        _shapeRenderer.color = insideColor;
        _shapeRenderer.sprite = shape.ShapeSprite;
        _insideOutlineRenderer.color = insideOutlineColor;
        _insideOutlineRenderer.sprite = shape.InsideOutlineSprite;
        _outsideOutlineRenderer.color = outsideOutlineColor;
        _outsideOutlineRenderer.sprite = shape.OutsideOutlineSprite;
        IsEmpty = true;
    }

    public bool Equals(ActionBar_Element other)
    {
        if (IsEmpty || other.IsEmpty)
            return false;

        return _iconRenderer.sprite.Equals(other._iconRenderer.sprite) &&
                _shapeRenderer.sprite.Equals(other._shapeRenderer.sprite) &&
                _insideOutlineRenderer.color.Equals(other._insideOutlineRenderer.color); 
    }


}
