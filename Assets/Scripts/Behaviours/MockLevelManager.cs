﻿using Game.Events;
using UnityEngine;

namespace Game.Behaviours
{
    public class MockLevelManager : MonoBehaviour
    {
        private void Start()
        {
            if (FindObjectOfType<LevelManager>() == null) UIEvent<SceneReadyEvent>.Instance.Invoke();
        }
    }
}