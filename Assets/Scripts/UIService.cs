using UnityEngine;

public class UIService : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ActionBar _actionBar;
    [SerializeField] private PieceSpawner _pieceSpawner;

    [Header("Windows")]
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _looseWindow;

    private void Start()
    {
        _actionBar.winEvent += WinWindowOpen;
        _actionBar.looseEvent += LooseWindowOpen;
    }


    private void LooseWindowOpen()
    {
        _looseWindow.SetActive(true);
    }

    private void WinWindowOpen()
    {
        _winWindow.SetActive(true);
    }
}
