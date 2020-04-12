using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Game.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;
using Application = UnityEngine.Application;



[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
public class BoardSettings : ScriptableObject
{


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
    
    public TileData GetTileDataAt(int x, int y)
    {
        return _tileBehaviours.Tiles[x * _height + y];
    }

    public void SaveSettings(TileData[,] tiles)
    {
        _tileBehaviours.Tiles.Clear();
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                foreach (var tile in tiles)
                {
                    _tileBehaviours.Tiles.Add(tile);
                }
            }
        }
        
        
        _data = new BoardSettingsData();
        _data.Width = _width;
        _data.Height = _height;
        _data.LevelName = _levelName;
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
        
        Debug.Log(_size);
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
    }
}
