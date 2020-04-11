using System;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public int ColorIndex => _colorIndex;

    public Vector2 Size => _size;

    [SerializeField]
    private int _colorIndex;
    
    [SerializeField]
    private Vector2 _size;
    private Vector2Int _coordinate;
    
}