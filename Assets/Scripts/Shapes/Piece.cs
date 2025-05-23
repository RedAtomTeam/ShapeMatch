using System;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private GameObject _shapeParent;
    [SerializeField] private SpriteRenderer _iconRenderer;

    private Shape _shape;

    public Shapes ShapeType { get; private set; }
    public Color OutlineColor { get; private set; }
    public Sprite Icon { get; private set; }

    
    public void Init(Color color, Shape shape, Sprite icon, PieceSpawner spawner)
    {
        _shape = Instantiate(shape, _shapeParent.transform);
        _shape.SetOutlineColor(color);
        _iconRenderer.sprite = icon;

        ShapeType = _shape.shapeType;
        OutlineColor = color;
        Icon = icon;

        spawner.spawnIsDone += Activate;
        spawner.startSpawn += Deactivate;
    }

    private void Activate()
    {

    }

    private void Deactivate()
    {

    }

    public Bounds GetBounds()
    {
        return _shape.GetBounds();
    }


}
