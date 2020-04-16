using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Behaviours
{
    [RequireComponent(typeof(BoardBehaviour))]
    public class BoardInputBehaviour : MonoBehaviour
    {
        // private static readonly IInput Input = new MouseInput();

        private BoardBehaviour _board;

        private void Start()
        {
            _board = GetComponent<BoardBehaviour>();
            Debug.Log(_board.Tiles);
            RefreshTiles(_board.Tiles);
            _board.onTileCreated += ListenToTileClick;
            _board.onTileDestroyed += DoNotListenToTileClick;
        }

        private void ListenToTileClick(TileBehaviour tile)
        {
            tile.onClick += OnTileClicked;
        }

        private void DoNotListenToTileClick(TileBehaviour tile)
        {
            tile.onClick -= OnTileClicked;
        }

        private void RefreshTiles(TileBehaviour[,] tiles)
        {
            for (var i = 0; i < tiles.GetLength(0); i++)
            for (var j = 0; j < tiles.GetLength(1); j++)
                tiles[i, j].onClick += OnTileClicked;
        }

        private void OnTileClicked(TileBehaviour tile)
        {
            if (tile.gameObject.activeInHierarchy && !EventSystem.current.IsPointerOverGameObject())
            {
                if (tile.PowerUp == null)
                    _board.OnTileMatched(tile);
                else
                    _board.OnTileActivated(tile);
            }
        }
    }
}


public interface IInput
{
    bool GetInputDown();
    bool GetInputUp();

    Vector3 GetPointerPosition();
}

public class MouseInput : IInput
{
    public bool GetInputUp()
    {
        return Input.GetMouseButtonUp(0);
    }

    public Vector3 GetPointerPosition()
    {
        return Input.mousePosition;
    }

    bool IInput.GetInputDown()
    {
        return Input.GetMouseButtonDown(1);
    }
}