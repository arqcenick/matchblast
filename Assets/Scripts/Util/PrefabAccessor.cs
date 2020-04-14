using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using Game.Behaviours;



namespace Game.Util
{

    
    public static class SimpleObjectPool
    {
        public const int MaxPoolSize = 1024;

        private static Queue<MonoBehaviour> _objectPool = new Queue<MonoBehaviour>(MaxPoolSize);

        private static int _canRecycled = 0;
        
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour
        {

            if (_objectPool.Count > 0)
            {
                var newObject = (T) _objectPool.Dequeue();
                newObject.transform.SetParent(parent);
                newObject.transform.SetPositionAndRotation(position, rotation);
                return newObject;
            }
            else
            {
                return Object.Instantiate(original, position, rotation, parent);
            }
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
    
        public static PrefabAccessor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PrefabAccessor>();
                }
                return _instance;
            }
        }

        private static PrefabAccessor _instance;


        public List<TileBehaviour> Prefabs => _prefabs;
        public List<Sprite> PowerUpSprites => _powerUpSprites;
    
        public List<Sprite> TileSprites => _tileSprites;

        [SerializeField]
        private List<TileBehaviour> _prefabs;
    
        [SerializeField]
        private List<Sprite> _powerUpSprites;
        [SerializeField]
        private List<Sprite> _tileSprites;
    }

}

