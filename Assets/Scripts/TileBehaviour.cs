using System;
using System.Collections;
using UnityEngine;
using  DG.Tweening;
using DG.Tweening.Core.Easing;

public class TileBehaviour : MonoBehaviour
{

    public Action<TileBehaviour> onClick;
    public int ColorIndex => _colorIndex;
    public bool Destroyed => _destroyed;

    public Vector2Int Coordinate => _coordinate;

    public Vector2 Size => _size;

    [SerializeField]
    private int _colorIndex;
    
    [SerializeField]
    private Vector2 _size;
    private Vector2Int _coordinate;


    private bool _destroyed;
    
    public void DestroyTile()
    {
        _destroyed = true;
        StartCoroutine(DeathAnimation());
    }

    private IEnumerator DeathAnimation()
    {
        yield return null;
        Destroy(gameObject);
    }
    
    
    private void OnMouseDown()
    {
        onClick(this);
    }


    
    
    public void SetPosition(Vector2 position)
    {
        DOTween.defaultEaseType = Ease.InQuad;
        transform.DOMove(position, 0.5f);
    }

    public void SetCoordinate(Vector2Int vector2Int)
    {
        _coordinate = vector2Int;
    }
}