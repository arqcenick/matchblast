﻿using System;
using System.Collections;
using DG.Tweening;
using Game.Behaviours;
using Game.Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


namespace Game.UI
{
    public class LevelEndUI : MonoBehaviour
    {
        
        [SerializeField]
        private Image _darkeningPanel;

        [SerializeField]
        private WinLevelUi _winLevelPanel;

        private BoardBehaviour _board;
        
        private void Awake()
        {
            _board = FindObjectOfType<BoardBehaviour>();
            _darkeningPanel.enabled = false;
            _darkeningPanel.color = Color.clear;
            _board.onPlayerWin += ShowWinGame;
        }

        private void ShowWinGame(int starCount)
        {
            DarkenPanel();
            _winLevelPanel.transform.DOMoveX(0, 1f).OnComplete(()=>StartCoroutine(Win(starCount)));
            
        }

        private IEnumerator Win(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _winLevelPanel.AddStar();
                yield return new WaitForSeconds(1);
            }
        }

        private void ShowLoseGame()
        {
            
        }

        private void DarkenPanel()
        {
            _darkeningPanel.enabled = true;
            _darkeningPanel.DOFade(0.65f, 1f);
        }
        
    }

}
