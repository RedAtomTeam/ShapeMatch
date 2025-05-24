using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private GameObject _shapeParent;
    [SerializeField] private SpriteRenderer _iconRenderer;

    private Shape _shape;
    private bool _enabled = false;

    private ActionBar _actionBar;
    private PieceSpawner _pieceSpawner;
    
    public Sprite IconSprite { get => _iconRenderer.sprite; }
    public Sprite ShapeSprite { get => _shape.ShapeSprite;}
    public Sprite OutlineSprite { get => _shape.OutlineSprite; }
    public Color OutlineColor { get => _shape.OutlineColor; }


    public void Init(Color color, Shape shape, Sprite icon, PieceSpawner spawner, ActionBar actionBar)
    {
        _shape = Instantiate(shape, _shapeParent.transform);
        _shape.OutlineColor = color;
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
