using System;
using UnityEngine;

public class Shape : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer _shape;
    [SerializeField] private SpriteRenderer _insideOutline;
    [SerializeField] private SpriteRenderer _outsideOutline;

    public Sprite ShapeSprite { get => _shape.sprite; private set => _shape.sprite = value; }
    public Color InsideColor { get => _shape.color; set => _shape.color = value; }
    public Sprite InsideOutlineSprite { get => _insideOutline.sprite; private set => _insideOutline.sprite = value; }
    public Sprite OutsideOutlineSprite { get => _outsideOutline.sprite; private set => _outsideOutline.sprite = value; }

    public Color InsideOutlineColor { get => _insideOutline.color; set => _insideOutline.color = value; }
    public Color OutsideOutlineColor { get => _outsideOutline.color; set => _outsideOutline.color = value; }


    public Bounds GetBounds()
    {
        Collider2D[] colliders;
        Bounds totalBounds = new Bounds();
        bool hasBounds = false;
        colliders = gameObject.GetComponentsInChildren<Collider2D>();

        if (colliders.Length == 0)
            throw new Exception("No Colliders");
        
        foreach (var collider in colliders)
        {
            if (!hasBounds)
            {
                totalBounds = GetColliderLocalBounds(collider);
                hasBounds = true;
            }
            else
            {
                totalBounds.Encapsulate(GetColliderLocalBounds(collider));
            }
        }

        return totalBounds;
    }

    private Bounds GetColliderLocalBounds(Collider2D collider)
    {
        if (collider is BoxCollider2D box)
        {
            return new Bounds(box.offset, box.size);
        }
        else if (collider is CircleCollider2D circle)
        {
            float diameter = circle.radius * 2;
            return new Bounds(circle.offset, new Vector3(diameter, diameter, 0));
        }
        else if (collider is CapsuleCollider2D capsule)
        {
            return new Bounds(capsule.offset, capsule.size);
        }
        else if (collider is PolygonCollider2D poly)
        {
            Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 max = new Vector2(float.MinValue, float.MinValue);

            foreach (var point in poly.points)
            {
                min = Vector2.Min(min, point);
                max = Vector2.Max(max, point);
            }

            Vector2 size = max - min;
            Vector2 center = min + size * 0.5f;
            return new Bounds(center, size);
        }
        else
        {
            throw new NotSupportedException($"Collider type {collider.GetType()} not supported.");
        }
    }
}
