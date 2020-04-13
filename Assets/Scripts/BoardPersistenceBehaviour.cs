using System;
using System.Collections.Generic;
using Game.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Behaviours
{
    [RequireComponent(typeof(BoardBehaviour))]
    public class BoardPersistenceBehaviour : MonoBehaviour
    {
        [SerializeField]
        private GameObject Mask;
        private BoardBehaviour _board => GetComponent<BoardBehaviour>();

        public void CreateRandomBoard()
        {

            ClearBoard();

            _board.Tiles = new TileBehaviour[_board.Settings.Width,_board.Settings.Height];
            for (int i = 0; i < _board.Settings.Width; i++)
            {
                for (int j = 0; j < _board.Settings.Height; j++)
                {
                    _board.Tiles[i, j] = _board.GetRandomTile(i, j);
                }
            }
            
        }

        public void SaveBoard()
        {
            AcquireBoard();
            TileData[,] tiles = new TileData[_board.Tiles.GetLength(0), _board.Tiles.GetLength(1)];
            for (int i = 0; i < _board.Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < _board.Tiles.GetLength(1); j++)
                {
                    tiles[i, j] = new TileData(_board.Tiles[i,j].ColorIndex, _board.Tiles[i,j].Coordinate);;
                    // tiles[i, j].SetData(_board.Tiles[i,j].ColorIndex, _board.Tiles[i,j].transform.localPosition);
                }
            }
            _board.Settings.SaveSettings(tiles);
        }
        
        public void LoadBoard()
        {
            if (_board.Tiles != null)
            {
                ClearBoard();
            }
            
            _board.Settings.LoadSettings();
            
            _board.Tiles = new TileBehaviour[_board.Settings.Width,_board.Settings.Height];
            for (int i = 0; i < _board.Settings.Width; i++) {
                for (int j = 0; j < _board.Settings.Height; j++)
                {
                    var tile = _board.Settings.GetTileDataAt(i, j);
                    _board.Tiles[i, j] = Instantiate(PrefabAccessor.Instance.Prefabs[0], _board.GetWorldPosition(tile.Coordinate.x, tile.Coordinate.y), Quaternion.identity, transform);
                    _board.Tiles[i, j].ColorIndex = tile.PrefabIndex;
                    _board.Tiles[i, j].SetCoordinate(new Vector2Int(i,j));
                }
            }

        }
        
        public void ClearBoard()
        {
            while (transform.childCount>0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
            _board.Tiles  = null;
            Mask.transform.localScale = new Vector3(_board.Settings.Width * 0.5f + 0.1f, _board.Settings.Height * 0.5f + 0.1f, 1);
        }
        
        private void AcquireBoard()
        {
            Debug.Log(_board.Settings.Height);

            _board.Tiles = new TileBehaviour[_board.Settings.Width,_board.Settings.Height];
            for (int i = 0; i < transform.childCount; i++)
            {
                int x = i / _board.Settings.Height;
                int y = i % _board.Settings.Height;
                _board.Tiles[x,y] = transform.GetChild(i).GetComponent<TileBehaviour>();
                _board.Tiles[x,y].SetCoordinate(new Vector2Int(x,y));
            }
        }
        private void Awake()
        {
            ClearBoard();
            LoadBoard();
            SetBoardFrame();
        }

        private void SetBoardFrame()
        {
            
        }
    }
}