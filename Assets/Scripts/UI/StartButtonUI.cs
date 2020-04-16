using System;
using System.Collections;
using System.Collections.Generic;
using Game.Behaviours;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonUI : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(FindObjectOfType<LevelManager>().StartLevel);
    }
}
