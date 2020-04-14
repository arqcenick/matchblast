using System;
using System.Collections.Generic;
using Game.Behaviours;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Data
{
    [Serializable]
    public class TileDataList
    {
        public List<TileData> Tiles;
    }

    [Serializable]
    public class TileData
    {
        [FormerlySerializedAs("_position")] [SerializeField]
        private Vector2Int _coordinate;


        [SerializeField] private int _prefabIndex;

        public TileData(TileColor prefabIndex, Vector2Int coordinate)
        {
            _prefabIndex = (int) prefabIndex;
            _coordinate = coordinate;
        }

        public Vector2Int Coordinate => _coordinate;

        public TileColor PrefabIndex => (TileColor) _prefabIndex;
    }
}