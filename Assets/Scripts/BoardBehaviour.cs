using System;
using TMPro;
using UnityEngine;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

namespace Game.Behaviours
{
    public class BoardBehaviour : MonoBehaviour
    {

        public Action<TileBehaviour> onTileCreated;
        public TileBehaviour[,] Tiles
        {
            get => _tiles;
            set => _tiles = value;
        }

        public BoardSettings Settings => _settings;
        
        [SerializeField]
        private BoardSettings _settings;

        private TileBehaviour[,] _tiles;

        private bool[] _columnUpdateFlags;
        
        public void DestroyTileAt(Vector2Int coordinate)
        {
            _tiles[coordinate.x, coordinate.y].DestroyTile();
            UpdateColumn(coordinate.x);
        }
        
        public TileBehaviour GetRandomTile(int x, int y)
        {
            
            var randomTileIndex = Random.Range(0, PrefabAccessor.Instance.Prefabs.Count);
            var tilePrefab = PrefabAccessor.Instance.Prefabs[randomTileIndex];
            Vector2 position = GetWorldPosition(x, y);
            var tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
            tile.SetPosition(position);
            tile.SetCoordinate(new Vector2Int(x, y));
            onTileCreated?.Invoke(tile);
            return tile;
        }

        public Vector2 GetWorldPosition(int x, int y)
        {
            return new Vector2(x * _settings.Size.x, y * _settings.Size.y);
        }
        
        private void Awake()
        {
            _columnUpdateFlags = new bool[_settings.Width];
        }

        private void UpdateColumn(int columnIndex)
        {
            _columnUpdateFlags[columnIndex] = true;
        }
        
        private void Update()
        {
            for (int i = 0; i < _columnUpdateFlags.Length; i++)
            {
                if (_columnUpdateFlags[i])
                {
                    for (int j = 0; j < _settings.Height; j++)
                    {
                        if (_tiles[i, j].Destroyed)
                        {
                            for (int k = j; k < _settings.Height-1; k++)
                            {
                                _tiles[i, k] = _tiles[i, k + 1];
                                _tiles[i, k].SetPosition(GetWorldPosition(i,k));
                                _tiles[i,k].SetCoordinate(new Vector2Int(i,k));
                            }
                            _tiles[i, _settings.Height - 1] = GetRandomTile(i, _settings.Height - 1);
                            _tiles[i, _settings.Height - 1].transform.position = GetWorldPosition(i, _settings.Height);
                            _tiles[i, _settings.Height - 1].SetPosition(GetWorldPosition(i,_settings.Height - 1));
                            _tiles[i,_settings.Height - 1].SetCoordinate(new Vector2Int(i,_settings.Height - 1));

                            j--;
                            
                        }
                    }

                    _columnUpdateFlags[i] = false;
                }
            }
        }

        private void StartUpdateColumn(int columnIndex)
        {
            for (int i = 0; i < _settings.Height; i++)
            {
                var tile = _tiles[columnIndex, i];
                if (tile.Destroyed)
                {
                    
                }
            }
        }
        
    }
}
