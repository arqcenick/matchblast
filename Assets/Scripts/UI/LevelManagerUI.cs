using System;
using System.Collections;
using System.Collections.Generic;
using Game.Behaviours;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LevelManager))]
public class LevelManagerUI : MonoBehaviour
{

    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = GetComponent<LevelManager>();
    }

    public void StartLevel()
    {
        _levelManager.StartLevel();
    }
    
}
