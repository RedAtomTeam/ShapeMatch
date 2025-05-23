using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UI;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private Button refreshButton;

    [Header("Spawn Settings")]
    [SerializeField] private float width = 10f; 
    [SerializeField] private float height = 5f;
    [SerializeField] private bool randomizeX;
    [SerializeField] private bool randomizeY;
    [SerializeField] private LayerMask _obstacleLayerMask;
    [SerializeField] private float _secondsPerPiece;

    [Header("Pieces Settings")]
    [SerializeField] private Piece pref;
    [SerializeField] private int _uniqueElementCount;
    [SerializeField] private List<Shape> _shapes = new List<Shape>();
    [SerializeField] private List<Sprite> _icons = new List<Sprite>();
    [SerializeField] private List<Color> _colors = new List<Color>();

    private List<Piece> _pieces; 
    private readonly Collider2D[] _obstacles = new Collider2D[32];


    public event Action startSpawn;
    public event Action spawnIsDone;


    public bool Spawn(Color color, Shape shape, Sprite icon)
    {

        var objectBound = shape.GetBounds();

        Vector3 spawnPosition = transform.position;

        if (randomizeX)
            spawnPosition.x += UnityEngine.Random.Range(
                -width / 2 + objectBound.size.x / 2,
                width / 2 - objectBound.size.x / 2);

        if (randomizeY)
            spawnPosition.y += UnityEngine.Random.Range(
                -height / 2 + objectBound.size.y / 2,
                height / 2 - objectBound.size.y / 2);


        var pointA = new Vector2(spawnPosition.x - objectBound.size.x, spawnPosition.y + objectBound.size.y);
        var pointB = new Vector2(spawnPosition.x + objectBound.size.x, spawnPosition.y - objectBound.size.y);
        Physics2D.OverlapAreaNonAlloc(pointA, pointB, _obstacles, _obstacleLayerMask);

        if (Physics2D.OverlapAreaNonAlloc(pointA, pointB, _obstacles, _obstacleLayerMask) > 0)
        {
            return false;

        }

        var spawnedPiece = Instantiate(pref, transform);
        spawnedPiece.gameObject.transform.position = spawnPosition;
        spawnedPiece.Init(color, shape, icon, this);

        _pieces.Add(spawnedPiece);

        return true;
    }

    public bool Spawn(Piece piece)
    {
        var objectBound = piece.GetBounds();

        Vector3 spawnPosition = transform.position;

        if (randomizeX)
            spawnPosition.x += UnityEngine.Random.Range(
                -width / 2 + objectBound.size.x / 2,
                width / 2 - objectBound.size.x / 2);

        if (randomizeY)
            spawnPosition.y += UnityEngine.Random.Range(
                -height / 2 + objectBound.size.y / 2,
                height / 2 - objectBound.size.y / 2);

        var pointA = new Vector2(spawnPosition.x - objectBound.size.x, spawnPosition.y + objectBound.size.y);
        var pointB = new Vector2(spawnPosition.x + objectBound.size.x, spawnPosition.y - objectBound.size.y);
        Physics2D.OverlapAreaNonAlloc(pointA, pointB, _obstacles, _obstacleLayerMask);

        if (Physics2D.OverlapAreaNonAlloc(pointA, pointB, _obstacles, _obstacleLayerMask) > 0)
            return false;

        piece.gameObject.transform.position = spawnPosition;
        piece.gameObject.SetActive(true);
        return true;

    }

    private void Start()
    {
        spawnIsDone += AddRefreshListenerToButton;
        startSpawn += RemoveRefreshListenerToButton;

        _pieces = new List<Piece>(_shapes.Count * _icons.Count * _colors.Count * _uniqueElementCount);
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        bool done = true;
        for (int i = 0; i < _uniqueElementCount; i++)
        {
            foreach (Shape shape in _shapes)
            {
                foreach (Sprite icon in _icons)
                {
                    foreach (Color color in _colors)
                    {
                        do
                        {
                            done = Spawn(color, shape, icon);
                        } while (!done);

                        yield return new WaitForSeconds(_secondsPerPiece);
                    }
                }
            }
        }

        spawnIsDone?.Invoke();
    }

    private void AddRefreshListenerToButton()
    {
        refreshButton.onClick.AddListener(PerformRefresh);
    }

    private void RemoveRefreshListenerToButton()
    {
        refreshButton.onClick.RemoveListener(PerformRefresh);
    }

    private void PerformRefresh()
    {
        StartCoroutine(Refresh());
    }

    [Button]
    public IEnumerator Refresh()
    {
        startSpawn?.Invoke();
        foreach(Piece piece in _pieces)
        {
            piece.gameObject.SetActive(false);
        }

        bool done = true;

        foreach (Piece piece in _pieces)
        {
            do
            {
                done = Spawn(piece);
            } while (!done);
            yield return new WaitForSeconds(_secondsPerPiece);
        }

        spawnIsDone?.Invoke();
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
        Gizmos.DrawCube(transform.position, new Vector3(width, height, 0.1f));
    }
}
