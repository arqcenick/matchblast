using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Behaviours;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{

    [SerializeField]
    private Transform _panel;
    
    [SerializeField]
    private Transform _open;
    
    [SerializeField]
    private Transform _closed;

    private Vector3 _previousPosition;
    
    [SerializeField]
    private bool _isClosed = false;

    private LevelManager _manager;
    private void Awake()
    {
        _manager = FindObjectOfType<LevelManager>();
        if (_manager != null)
        {
            _manager.onLevelStart += Toggle;
            _manager.onLevelReady += Toggle;
            if (_isClosed)
            {
                _panel.transform.position = _closed.transform.position;
            }
        }
        

    }

    private void OnDestroy()
    {
        if (_manager != null) _manager.onLevelStart -= Toggle;
        if (_manager != null) _manager.onLevelReady -= Toggle;
    }

    private void Toggle()
    {
        _isClosed = _isClosed ? Move(_open) : Move(_closed);
    }

    private bool Move(Transform target)
    {
        _panel.transform.DOMoveY(target.position.y, 0.5f);
        return !_isClosed;
    }
}
