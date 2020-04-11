using UnityEngine;
using UnityEngine.WSA;

namespace Game.Data
{
    public class TileData
    {
        
        public Vector2 Position => _position;

        public int PrefabIndex => _prefabIndex;
        

        private int _prefabIndex;
        private Vector2 _position;
        
        public TileData(int prefabIndex, Vector2 position)
        {
            _prefabIndex = prefabIndex;
            _position = position;
        }



    }
}