using System;
using UnityEngine;
using Game.Data;
using Random = UnityEngine.Random;

namespace Game.Behaviours
{
    public class BoardBehaviour : MonoBehaviour
    {

        [SerializeField]
        private BoardSettings _settings;

        private TileBehaviour[,] _tiles;


        
        public void CreateRandomBoard()
        {
            if (_tiles != null)
            {
                ClearBoard();
            }
            _tiles = new TileBehaviour[_settings.Width,_settings.Height];
            for (int i = 0; i < _settings.Width; i++)
            {
                for (int j = 0; j < _settings.Height; j++)
                {
                    _tiles[i, j] = GetRandomTile(i, j);
                }
            }
        }

        public void SaveBoard()
        {
            TileData[,] tiles = new TileData[_tiles.GetLength(0), _tiles.GetLength(1)];
            for (int i = 0; i < _tiles.GetLength(0); i++)
            {
                for (int j = 0; j < _tiles.GetLength(1); j++)
                {
                    tiles[i,j] = new TileData(_tiles[i,j].ColorIndex, _tiles[i,j].transform.localPosition);
                }
            }
            _settings.OverrideSettings(tiles);

        }
        
        public void LoadBoard()
        {
            if (_tiles != null)
            {
                ClearBoard();
            }
            _tiles = new TileBehaviour[_settings.Width,_settings.Height];
            for (int i = 0; i < _settings.Width; i++) {
                for (int j = 0; j < _settings.Height; j++)
                {
                    var tile = _settings.TileBehaviours[i, j];
                    _tiles[i, j] = Instantiate(_settings.Prefabs[tile.PrefabIndex], tile.Position, Quaternion.identity, transform);
                }
            }
        }

        private TileBehaviour GetRandomTile(int x, int y)
        {
            var randomTileIndex = Random.Range(0, _settings.Prefabs.Count);
            var tilePrefab = _settings.Prefabs[randomTileIndex];
            Vector2 position = new Vector3(x * tilePrefab.Size.x, y * tilePrefab.Size.y);
            return  Instantiate(tilePrefab, position, Quaternion.identity, transform);
        }

        private void ClearBoard()
        {
            Debug.Log(_tiles.Length);
            for (int i = 0; i < _tiles.GetLength(0); i++)
            {
                for (int j = 0; j < _tiles.GetLength(1); j++)
                {
                    DestroyImmediate(_tiles[i,j].gameObject);

                }
            }
            _tiles  = null;
        }
        
        private void Awake()
        {
            if (_settings == null)
            {
                throw new NullReferenceException();
            }
            CreateRandomBoard();
        }


    }
}
