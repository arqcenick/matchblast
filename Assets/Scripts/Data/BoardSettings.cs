using System;
using System.Collections.Generic;
using System.Linq;
using Game.Behaviours;
using Game.Util;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
    public class BoardSettings : ScriptableObject
    {
        private BoardSettingsData _data;

        [SerializeField] private int _height;
        [SerializeField] private int _width;
        
        [SerializeField] private string _levelName;

        [SerializeField] private Dictionary<TileColor, int> _objectives;

        [SerializeField] private Vector2 _size;

        [SerializeField] private List<int> _starObjectives;
        
        private List<bool> _available;

        [SerializeField] private TileDataList _tileBehaviours;

        [SerializeField] private int _totalMoves;

        public List<int> StarObjectiveList => _starObjectives;

        public Dictionary<TileColor, int> Objectives => _objectives;

        public List<bool> Available => _available;

        public int TotalMoves => _totalMoves;

        public int Height => _height;

        public int Width => _width;

        public Vector2 Size => _size;

        public TileData GetTileDataAt(int x, int y)
        {
            return _tileBehaviours.Tiles[x * _height + y];
        }

        public void SaveSettings()
        {
            _data.LevelName = _levelName;
            _data.ObjectiveDict = new TileObjectiveDict();
            _data.ObjectiveDict.SetDictionary(_objectives);
            _data.Tiles = _tileBehaviours;
            _data.Size = _size;
            _data.TotalMoves = _totalMoves;
            _data.AvailableTiles = _available;
            _data.Height = _height;
            _data.Width = _width;
            var path = "Assets/Resources/Levels/" + _levelName + ".asset";

#if UNITY_EDITOR
            var asset = new TextAsset(JsonUtility.ToJson(_data));
            AssetDatabase.CreateAsset(asset, path);
#endif

        }

        public void SaveSettings(TileData[,] tiles)
        {
            _tileBehaviours.Tiles.Clear();

            foreach (var tile in tiles) _tileBehaviours.Tiles.Add(tile);

            _data = new BoardSettingsData();
            _data.Width = _width;
            _data.Height = _height;
            SaveSettings();
        }

        public void LoadSettings()
        {
            try
            {
                var path = "Levels/" + _levelName;
                var saveFile = Resources.Load<TextAsset>(path);
                var settingsData = JsonUtility.FromJson<BoardSettingsData>(saveFile.text);
                _tileBehaviours = settingsData.Tiles;
                _width = settingsData.Width;
                _height = settingsData.Height;
                _levelName = settingsData.LevelName;
                _size = settingsData.Size;
                _totalMoves = settingsData.TotalMoves;
                _objectives = settingsData.ObjectiveDict.AsDictionary();
                _available = settingsData.AvailableTiles;
            }
            catch
            {
                Debug.Log("failed to load");
                var path = "Levels/default_level";
                var saveFile = Resources.Load<TextAsset>(path);
                var settingsData = JsonUtility.FromJson<BoardSettingsData>(saveFile.text);
                _tileBehaviours = settingsData.Tiles;
                _width = settingsData.Width;
                _height = settingsData.Height;
                _levelName = settingsData.LevelName;
                _size = settingsData.Size;
                _totalMoves = settingsData.TotalMoves;
                _objectives = settingsData.ObjectiveDict.AsDictionary();
                _available = settingsData.AvailableTiles;
            }

        }

        public void SetObjectives(Dictionary<TileColor, int> objectives)
        {
            _objectives = objectives;
        }
        
        public void SetAvailable(List<bool> available)
        {
            _available = available;
        }

        [Serializable]
        public class BoardSettingsData
        {
            public int Height;
            public string LevelName;
            public TileObjectiveDict ObjectiveDict;
            public List<bool> AvailableTiles;
            public Vector2 Size;
            public StarObjectives StarObjectives;
            public TileDataList Tiles;
            public int TotalMoves;
            public int Width;
        }

        [Serializable]
        public class StarObjectives
        {
            public List<int> MinCounts;
        }

        [Serializable]
        public class TileObjectiveDict
        {
            public List<TileColor> Colors;
            public List<int> Counts;

            public void SetDictionary(Dictionary<TileColor, int> objectives)
            {
                Colors = objectives.Keys.ToList();
                Counts = objectives.Values.ToList();
            }

            public Dictionary<TileColor, int> AsDictionary()
            {
                var result = new Dictionary<TileColor, int>();
                for (var i = 0; i < Colors.Count; i++) result.Add(Colors[i], Counts[i]);

                return result;
            }
        }

        public static BoardSettings GetRandomLevel()
        {
            var settings = PrefabAccessor.Instance.LevelTemplate;
            settings._width = UnityEngine.Random.Range(6, 10);
            settings._height = UnityEngine.Random.Range(6, 10);
            int requiredTurns = UnityEngine.Random.Range(20, 40);
            settings._starObjectives = new List<int>(){ requiredTurns-15, requiredTurns-13, requiredTurns-10};
            var objectives = new Dictionary<TileColor, int>();
            
            for (int i = 0; i < 5; i++)
            {
                objectives.Add((TileColor)i, UnityEngine.Random.Range(0,15));
            }

            settings._objectives = objectives;
            return settings;
        }
    }
}