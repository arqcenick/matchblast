using System;
using System.Collections.Generic;
using System.Linq;
using Game.Behaviours;
using UnityEditor;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
    public class BoardSettings : ScriptableObject
    {
        private BoardSettingsData _data;

        [SerializeField] private int _height;

        [SerializeField] private string _levelName;

        [SerializeField] private Dictionary<TileColor, int> _objectives;

        [SerializeField] private Vector2 _size;

        [SerializeField] private List<int> _starObjectives;


        [SerializeField] private TileDataList _tileBehaviours;

        [SerializeField] private int _totalMoves;

        [SerializeField] private int _width;
        public List<int> StarObjectiveList => _starObjectives;

        public Dictionary<TileColor, int> Objectives => _objectives;

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
            }
            catch
            {
                Debug.Log("failed to load");
            }
        }

        public void SetObjectives(Dictionary<TileColor, int> objectives)
        {
            _objectives = objectives;
        }

        [Serializable]
        public class BoardSettingsData
        {
            public int Height;
            public string LevelName;
            public TileObjectiveDict ObjectiveDict;
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
    }
}