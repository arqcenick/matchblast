using System;
using System.Collections.Generic;
using Game.Behaviours;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.WSA;

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
        public Vector2Int Coordinate => _coordinate;

        public TileColor PrefabIndex => (TileColor) _prefabIndex;
        
        
        [SerializeField]
        private int _prefabIndex;
        [FormerlySerializedAs("_position")] [SerializeField]
        private Vector2Int _coordinate;
        
        public TileData(TileColor prefabIndex, Vector2Int coordinate)
        {
            _prefabIndex = (int) prefabIndex;
            _coordinate = coordinate;
        }
        
    }
}