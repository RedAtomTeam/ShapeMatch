using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private GameObject _shapeParent;
    [SerializeField] private SpriteRenderer _iconRenderer;
    [SerializeField] private Color _innerShadowColor;
    [SerializeField] private float _innerShadowDivide;


    private Shape _shape;
    private bool _enabled = false;

    private ActionBar _actionBar;
    private PieceSpawner _pieceSpawner;


    
    public Sprite IconSprite { get => _iconRenderer.sprite; }
    public Sprite ShapeSprite { get => _shape.ShapeSprite;}
    public Sprite InsideOutlineSprite { get => _shape.InsideOutlineSprite; }
    public Sprite OutsideOutlineSprite { get => _shape.OutsideOutlineSprite; }
    public Color InsideOutlineColor { get => _shape.InsideOutlineColor; }
    public Color OutsideOutlineColor { get => _shape.OutsideOutlineColor; }
    public Color InsideColor { get => _shape.InsideColor; }

    public void Init(Color color, Shape shape, Sprite icon, PieceSpawner spawner, ActionBar actionBar)
    {
        _shape = Instantiate(shape, _shapeParent.transform);
        _shape.InsideColor = color;
        _shape.InsideOutlineColor = _innerShadowColor + color / _innerShadowDivide;
        _iconRenderer.sprite = icon;



        _actionBar = actionBar;

        spawner.spawnIsDone += Activate;
        spawner.startSpawn += Deactivate;

        _pieceSpawner = spawner;
    }

    private void Take()
    {
        if (_actionBar.TryAdd(this))
        {
            _pieceSpawner.Remove(this);
            Destroy(gameObject);
        }
    }

    private void Activate()
    {
        _enabled = true;
    }

    private void Deactivate()
    {
        _enabled = false;
    }

    public Bounds GetBounds()
    {
        return _shape.GetBounds();
    }

    private void OnMouseDown()
    {
        if (_enabled)
            Take();
    }
}
