﻿using System;
using System.Collections.Generic;
using Game.Data;
using Game.Events;
using Game.Util;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Behaviours
{
    public class BoardBehaviour : MonoBehaviour
    {
        private bool[] _columnUpdateFlags;

        private Dictionary<TileColor, int> _gameObjectives;

        private int _movesLeft;

        [SerializeField] private BoardSettings _settings;

        public Action<Dictionary<TileColor, int>, int> onLevelStarted;

        public Action onPlayerLose;

        public Action<int> onPlayerMoved;

        public Action<int> onPlayerWin;

        //Events
        public Action<TileBehaviour> onTileCreated;

        public Action<TileBehaviour> onTileDestroyed;

        public TileBehaviour[,] Tiles { get; set; }

        public BoardSettings Settings
        {
            get => _settings;
            set => _settings = value;
        }


        private void DestroyTileAt(Vector2Int coordinate)
        {
            Tiles[coordinate.x, coordinate.y].DestroyTile();
            UpdateColumn(coordinate.x);
            onTileDestroyed(Tiles[coordinate.x, coordinate.y]);
        }

        public TileBehaviour GetRandomTile(int x, int y)
        {
            var tilePrefab = PrefabAccessor.Instance.Prefabs[0];
            var position = GetWorldPosition(x, y);
            var tile = SimpleObjectPool.Instantiate(tilePrefab, position, Quaternion.identity, transform);
            tile.SetCoordinate(new Vector2Int(x, y));
            var randomTileIndex = Random.Range(0, 5);
            tile.ColorIndex = (TileColor) randomTileIndex;
            onTileCreated?.Invoke(tile);
            return tile;
        }

        public bool HasMatch(TileBehaviour originTile)
        {
            var neighbors = GetNeighbours(originTile.Coordinate);
            foreach (var neighbor in neighbors)
                if (neighbor.ColorIndex == originTile.ColorIndex && originTile.ColorIndex != TileColor.None &&
                    neighbor.Coordinate != originTile.Coordinate)
                    return true;
            return false;
        }

        public List<TileBehaviour> GetNeighbours(Vector2Int tilePosition, int order = 1)
        {
            if (order == 0) return new List<TileBehaviour> {Tiles[tilePosition.x, tilePosition.y]};

            var result = new List<TileBehaviour>();
            result.Add(Tiles[tilePosition.x, tilePosition.y]);
            if (tilePosition.x < _settings.Width - 1)
            {
                var targetTile = Tiles[tilePosition.x + 1, tilePosition.y];
                result.AddRange(GetNeighbours(targetTile.Coordinate, order - 1));
            }

            if (tilePosition.x > 0)
            {
                var targetTile = Tiles[tilePosition.x - 1, tilePosition.y];
                result.AddRange(GetNeighbours(targetTile.Coordinate, order - 1));
            }

            if (tilePosition.y < _settings.Height - 1)
            {
                var targetTile = Tiles[tilePosition.x, tilePosition.y + 1];
                result.AddRange(GetNeighbours(targetTile.Coordinate, order - 1));
            }

            if (tilePosition.y > 0)
            {
                var targetTile = Tiles[tilePosition.x, tilePosition.y - 1];
                result.AddRange(GetNeighbours(targetTile.Coordinate, order - 1));
            }

            return result;
        }

        public int DestroyRecursively(Vector2Int tilePosition)
        {
            var destroyedCount = 1;
            var originTile = Tiles[tilePosition.x, tilePosition.y];
            DestroyTileAt(tilePosition);
            var neighbours = GetNeighbours(tilePosition);
            foreach (var neighbour in neighbours)
                if (!neighbour.Destroyed && neighbour.ColorIndex == originTile.ColorIndex)
                    destroyedCount += DestroyRecursively(neighbour.Coordinate);

            return destroyedCount;
        }

        public void CreatePowerUpAtTile(TileBehaviour tile, int destroyedTiles)
        {
            if (destroyedTiles >= 9)
                tile.SetAsPowerUp(PowerUpType.TNT);
            else if (destroyedTiles >= 7)
                tile.SetAsPowerUp(PowerUpType.Dynamite);
            else if (destroyedTiles >= 5) tile.SetAsPowerUp(PowerUpType.Bomb);
        }

        public Vector2 GetWorldPosition(int x, int y)
        {
            return new Vector2(x * _settings.Size.x, (y - _settings.Height * 0.5f) * _settings.Size.y + 0.5f)
                   - Vector2.right * (_settings.Width - 1) * 0.25f
                   + Vector2.up * transform.position.y;
        }

        private void Awake()
        {
            _columnUpdateFlags = new bool[_settings.Width];

            UIEvent<SceneReadyEvent>.Instance.AddListener(StartLevel);
        }

        private void OnDestroy()
        {
            UIEvent<SceneReadyEvent>.Instance.RemoveListener(StartLevel);
        }

        private void UpdateColumn(int columnIndex)
        {
            _columnUpdateFlags[columnIndex] = true;
        }

        public void StartLevel()
        {
            CheckMatches();
            _gameObjectives = new Dictionary<TileColor, int>();
            foreach (var kv in _settings.Objectives) _gameObjectives.Add(kv.Key, kv.Value);

            _movesLeft = _settings.TotalMoves;
            onLevelStarted(_gameObjectives, _movesLeft);
        }

        private void CheckWinLoseCondition()
        {
            _movesLeft = Mathf.Max(0, _movesLeft - 1);
            onPlayerMoved(_movesLeft);
            var allObjectivesReached = true;
            foreach (var kv in _gameObjectives) allObjectivesReached &= kv.Value <= 0;
            if (allObjectivesReached)
                onPlayerWin(GetStarCount());
            else if (_movesLeft == 0) onPlayerLose();
        }

        private int GetStarCount()
        {
            for (var i = 0; i < _settings.StarObjectiveList.Count; i++)
                if (_movesLeft < _settings.StarObjectiveList[i])
                    return i;
            return 3;
        }

        private void Update()
        {
            var shouldCheckWinCondition = false;
            for (var i = 0; i < _columnUpdateFlags.Length; i++)
                if (_columnUpdateFlags[i])
                {
                    shouldCheckWinCondition = true;
                    var counter = 0;
                    for (var j = 0; j < _settings.Height; j++)
                        if (Tiles[i, j].Destroyed)
                        {
                            counter++;
                            for (var k = j; k < _settings.Height - 1; k++)
                            {
                                Tiles[i, k] = Tiles[i, k + 1];
                                Tiles[i, k].SetPosition(GetWorldPosition(i, k));
                                Tiles[i, k].SetCoordinate(new Vector2Int(i, k));
                            }

                            Tiles[i, _settings.Height - 1] = GetRandomTile(i, _settings.Height - 1);
                            Tiles[i, _settings.Height - 1].transform.position =
                                GetWorldPosition(i, _settings.Height - 1 + counter);
                            Tiles[i, _settings.Height - 1].SetPosition(GetWorldPosition(i, _settings.Height - 1));
                            Tiles[i, _settings.Height - 1].SetCoordinate(new Vector2Int(i, _settings.Height - 1));

                            j--;
                        }

                    _columnUpdateFlags[i] = false;
                    CheckMatches();
                }

            if (shouldCheckWinCondition) CheckWinLoseCondition();
        }

        private void CheckMatches()
        {
            foreach (var tile in Tiles) tile.MatchType = MatchType.None;

            var matchGroups = new List<List<Vector2Int>>();
            foreach (var tile in Tiles)
                if (HasMatch(tile) && tile.MatchType == MatchType.None)
                {
                    var matchGroup = new List<Vector2Int>();
                    CheckMatchesRecursively(matchGroup, tile);

                    for (var i = 0; i < matchGroup.Count; i++)
                        Tiles[matchGroup[i].x, matchGroup[i].y].MatchType = GetMatchTypeForCount(matchGroup.Count);
                    matchGroups.Add(matchGroup);
                }

            // foreach (var matchGroup in matchGroups)
            // {
            //     foreach (var coordinate in matchGroup)
            //     {
            //         _tiles[coordinate.x, coordinate.y].transform.localScale *= 0.5f;
            //     }
            // }
        }

        private MatchType GetMatchTypeForCount(int count)
        {
            if (count < 5) return MatchType.Empty;

            if (count < 7) return MatchType.Bomb;

            if (count < 9) return MatchType.Dynamite;

            return MatchType.TNT;
        }

        private void CheckMatchesRecursively(List<Vector2Int> matchGroup, TileBehaviour tile)
        {
            matchGroup.Add(tile.Coordinate);
            tile.MatchType = MatchType.Undetermined;
            var neighbours = GetNeighbours(tile.Coordinate);
            foreach (var neighbour in neighbours)
                if (neighbour.MatchType == MatchType.None && neighbour.ColorIndex == tile.ColorIndex &&
                    tile.ColorIndex != TileColor.None)
                    CheckMatchesRecursively(matchGroup, neighbour);
        }


        public void OnTileMatched(TileBehaviour tile)
        {
            if (HasMatch(tile))
            {
                var destroyed = DestroyRecursively(tile.Coordinate);
                CreatePowerUpAtTile(tile, destroyed);
            }
        }

        public void OnTileActivated(TileBehaviour tile)
        {
            if (tile.PowerUp != PowerUpType.None)
            {
                var powerUp = tile.PowerUp;
                tile.ClearPowerUp();
                switch (powerUp)
                {
                    case PowerUpType.Bomb:
                        DestroyTileAt(tile.Coordinate);
                        var neigbours = GetNeighbours(tile.Coordinate);
                        foreach (var neigbour in neigbours)
                            if (neigbour.PowerUp == PowerUpType.None)
                                DestroyTileAt(neigbour.Coordinate);
                            else
                                OnTileActivated(neigbour);
                        break;
                    case PowerUpType.Dynamite:
                        DestroyTileAt(tile.Coordinate);
                        neigbours = GetNeighbours(tile.Coordinate, 2);
                        foreach (var neigbour in neigbours)
                            if (neigbour.PowerUp == PowerUpType.None)
                                DestroyTileAt(neigbour.Coordinate);
                            else
                                OnTileActivated(neigbour);

                        break;
                    case PowerUpType.TNT:
                        DestroyTileAt(tile.Coordinate);
                        neigbours = GetNeighbours(tile.Coordinate, 3);
                        foreach (var neigbour in neigbours)
                            if (neigbour.PowerUp == PowerUpType.None)
                                DestroyTileAt(neigbour.Coordinate);
                            else
                                OnTileActivated(neigbour);
                        break;
                    case PowerUpType.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}