using UnityEngine;
using UnityEngine.UI;

public class ActionBar_Element : MonoBehaviour
{

    [SerializeField] private Image _iconRenderer;
    [SerializeField] private Image _shapeRenderer;
    [SerializeField] private Image _outlineRenderer;
    
    private bool _isEmpty = true;
    public bool IsEmpty { get => _isEmpty; private set => _isEmpty = value; }


    public void Put(Piece piece)
    {
        _iconRenderer.color = new Color(1, 1, 1, 1);
        _iconRenderer.sprite = piece.IconSprite;
        _shapeRenderer.color = new Color(1, 1, 1, 1);
        _shapeRenderer.sprite = piece.ShapeSprite;
        _outlineRenderer.color = piece.OutlineColor;
        _outlineRenderer.sprite = piece.OutlineSprite;

        IsEmpty = false;
    }

    public void Pop(Color color, Shape shape) 
    {
        _iconRenderer.color = new Color(0, 0, 0, 0);
        _shapeRenderer.color = color;
        _shapeRenderer.sprite = shape.ShapeSprite;
        _outlineRenderer.color = color;
        _outlineRenderer.sprite = shape.OutlineSprite;

        IsEmpty = true;
    }

    public bool Equals(ActionBar_Element other)
    {
        if (IsEmpty || other.IsEmpty)
            return false;

        return _iconRenderer.sprite.Equals(other._iconRenderer.sprite) &&
                _shapeRenderer.sprite.Equals(other._shapeRenderer.sprite) &&
                _outlineRenderer.color.Equals(other._outlineRenderer.color); 
    }


}
