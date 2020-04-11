using System.Collections;
using System.Collections.Generic;
using Game.Data;
using UnityEngine;


namespace Game.Behaviours
{
}

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
public class BoardSettings : ScriptableObject
{
    public List<TileBehaviour> Prefabs => _prefabs;

    public TileData[,] TileBehaviours => _tileBehaviours;

    public bool OverrideRandom => _overrideRandom;

    public int Height => _height;

    public int Width => _width;
    
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;

    [SerializeField]
    private bool _overrideRandom;
    
    private TileData[,] _tileBehaviours;

    [SerializeField]
    private List<TileBehaviour> _prefabs;

    public void OverrideSettings(TileData[,] tiles)
    {
        _overrideRandom = true;
        _tileBehaviours = tiles;
        _width = tiles.GetLength(0);
        _height = tiles.GetLength(1);
    }
}
