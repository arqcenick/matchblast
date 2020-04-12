using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{

    [SerializeField]
    private Transform _open;
    
    [SerializeField]
    private Transform _closed;

    private bool _isClosed = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            _isClosed = _isClosed ? Move(_open) : Move(_closed);
        }
    }

    public bool Move(Transform target)
    {
        transform.DOMoveY(target.position.y, 0.5f);
        return !_isClosed;
    }
}
