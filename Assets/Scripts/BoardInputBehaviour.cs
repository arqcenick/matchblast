using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Behaviours;
using UnityEngine;


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
        }

        private void ListenToTileClick(TileBehaviour tile)
        {
            tile.onClick += OnTileClicked;
        }

        private void RefreshTiles(TileBehaviour[,] tiles)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j].onClick += OnTileClicked;
                }
            }
        }

        private void OnTileClicked(TileBehaviour tile)
        {
           _board.DestroyTileAt(tile.Coordinate);

        }


        // private void Update()
        // {
        //     if (Input.GetInputDown())
        //     {
        //         PressOnBoard();
        //     }
        // }
        //
        // private void PressOnBoard()
        // {
        //     var position = Input.GetPointerPosition();
        // }
        //
        // private Vector2Int ConvertPointerPositionToBoardPosition(Vector3 pos)
        // {
        //     Vector3 absolutePosition = pos - transform.position;
        // }
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