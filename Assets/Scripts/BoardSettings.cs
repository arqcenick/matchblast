using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game.Behaviours;
using Game.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;
using Application = UnityEngine.Application;



[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
public class BoardSettings : ScriptableObject
{
    public Dictionary<TileColor, int> Objectives => _objectives;

    public int Height => _height;

    public int Width => _width;

    public Vector2 Size => _size;
     
    [SerializeField]
    private Vector2 _size;
    
    [SerializeField]
    private int _width;
    
    [SerializeField]
    private int _height;
    
    
    [SerializeField]
    private TileDataList _tileBehaviours;
    
    [SerializeField] 
    private string _levelName;

    private BoardSettingsData _data;
    
    [SerializeField]
    private Dictionary<TileColor, int> _objectives;
    

    public TileData GetTileDataAt(int x, int y)
    {
        return _tileBehaviours.Tiles[x * _height + y];
    }

    public void SaveSettings(TileData[,] tiles)
    {
        _tileBehaviours.Tiles.Clear();

        foreach (var tile in tiles)
        {
            _tileBehaviours.Tiles.Add(tile);
        }
        
        _data = new BoardSettingsData();
        _data.Width = _width;
        _data.Height = _height;
        _data.LevelName = _levelName;
        _data.ObjectiveDict = new TileObjectiveDict();
        _data.ObjectiveDict.SetDictionary(_objectives);
        _data.Tiles = _tileBehaviours;
        _data.Size = _size;
        string path = "Assets/Resources/Levels/" + _levelName + ".asset";

        TextAsset asset = new TextAsset(JsonUtility.ToJson(_data));
        AssetDatabase.CreateAsset(asset, path);
        Debug.Log(_size);

    }

    public void LoadSettings()
    {
        string path = "Levels/" + _levelName;
        var saveFile = Resources.Load <TextAsset>(path);
        Debug.Log(saveFile.text);
        var settingsData = JsonUtility.FromJson<BoardSettingsData>(saveFile.text);
        _tileBehaviours = settingsData.Tiles;
        _width = settingsData.Width;
        _height = settingsData.Height;
        _levelName = settingsData.LevelName;
        _size = settingsData.Size;
        _objectives = settingsData.ObjectiveDict.AsDictionary();
        
        Debug.Log(_tileBehaviours.Tiles.Count);
        try
        {

            
        }
        catch
        {
            Debug.Log("failed to load");
        }

    }

    [Serializable]
    public class BoardSettingsData
    {
        public int Width;
        public int Height;
        public string LevelName;
        public TileDataList Tiles;
        public Vector2 Size;
        public TileObjectiveDict ObjectiveDict;
    }
    
    [Serializable]
    public class TileObjectiveDict
    {
        public void SetDictionary(Dictionary<TileColor, int> objectives)
        {
            Colors = objectives.Keys.ToList();
            Counts = objectives.Values.ToList();
        }

        public Dictionary<TileColor, int> AsDictionary()
        {
            Dictionary<TileColor,int> result = new Dictionary<TileColor, int>();
            for (int i = 0; i < Colors.Count; i++)
            {
                result.Add(Colors[i], Counts[i]);
            }

            return result;
        }
        
        public List<TileColor> Colors;
        public List<int> Counts;
    }

    public void SetObjectives(Dictionary<TileColor, int> objectives)
    {
        _objectives = objectives;
    }
}
