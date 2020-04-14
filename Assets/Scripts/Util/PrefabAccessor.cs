using System.Collections.Generic;
using Game.Behaviours;
using UnityEngine;

namespace Game.Util
{
    public static class SimpleObjectPool
    {
        public const int MaxPoolSize = 1024;

        private static readonly Queue<MonoBehaviour> _objectPool = new Queue<MonoBehaviour>(MaxPoolSize);


        public static void Reset()
        {
            _objectPool.Clear();
        }

        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent)
            where T : MonoBehaviour
        {
            if (_objectPool.Count > 0)
            {
                var newObject = (T) _objectPool.Dequeue();
                newObject.transform.SetParent(parent);
                newObject.transform.SetPositionAndRotation(position, rotation);
                return newObject;
            }

            return Object.Instantiate(original, position, rotation, parent);
        }

        public static void Destroy(MonoBehaviour go)
        {
            if (_objectPool.Count < MaxPoolSize)
            {
                go.gameObject.SetActive(false);
                _objectPool.Enqueue(go);
            }
            else
            {
                Object.Destroy(go);
            }
        }
    }

    [ExecuteInEditMode]
    public class PrefabAccessor : MonoBehaviour
    {
        private static PrefabAccessor _instance;

        [SerializeField] private List<Sprite> _powerUpSprites;

        [SerializeField] private List<TileBehaviour> _prefabs;

        [SerializeField] private List<Sprite> _tileSprites;

        public static PrefabAccessor Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<PrefabAccessor>();
                return _instance;
            }
        }


        public List<TileBehaviour> Prefabs => _prefabs;
        public List<Sprite> PowerUpSprites => _powerUpSprites;

        public List<Sprite> TileSprites => _tileSprites;
    }
}