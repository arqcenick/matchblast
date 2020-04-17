using System;
using System.Collections;
using DG.Tweening;
using Game.Behaviours;
using Game.Events;
using Game.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class LevelEndUI : MonoBehaviour
    {
        private BoardBehaviour _board;

        [SerializeField] private Image _darkeningPanel;

        [SerializeField] private LoseLevelUI _loseLevelPanel;

        [SerializeField] private WinLevelUi _winLevelPanel;
        
        [SerializeField] private NoLifeUI _noLifePanel;


        private void Awake()
        {
            _board = FindObjectOfType<BoardBehaviour>();
            _darkeningPanel.enabled = false;
            _darkeningPanel.color = Color.clear;
        }

        private void Start()
        {
            _board.onPlayerWin += ShowWinGame;
            _board.onPlayerLose += ShowLoseGame;
        }


        private bool _timerStarted;
        private float _timer;
        private void Update()
        {


            if (_timerStarted)
            {
                _timer += Time.deltaTime;
                if (_timer > 3f)
                {
                    _timerStarted = false;
                    _timer = 0;
                    LightenPanel(); 
                    UIEvent<HidePlayerCounters>.Instance.Invoke();

                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UIEvent<MainMenuEvent>.Instance.Invoke();
                    
                }
                
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DarkenPanel();
                UIEvent<ShowPlayerCounters>.Instance.Invoke();
                _timerStarted = true;
            }
        }

        private void ShowWinGame(int starCount)
        {
            DarkenPanel();
            UIEvent<ShowPlayerCounters>.Instance.Invoke();
            _winLevelPanel.transform.DOMoveX(0, 1f).OnComplete(() => StartCoroutine(Win(starCount)));
        }

        private IEnumerator Win(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _winLevelPanel.AddStar();
                yield return new WaitForSeconds(1);
            }
        }

        private void ShowLoseGame()
        {
            DarkenPanel();
            if (PlayerServices.GetPlayerHealth() == 0)
            {
                _noLifePanel.transform.DOMoveX(0, 1f);
            }
            else
            {
                _loseLevelPanel.SetHealthVisibilities();
                _loseLevelPanel.transform.DOMoveX(0, 1f).OnComplete(_loseLevelPanel.LoseHealth);
            }

        }

        private void DarkenPanel()
        {
            
            _darkeningPanel.enabled = true;
            _darkeningPanel.DOFade(0.65f, 1f);
        }
        
        private void LightenPanel()
        {
            _darkeningPanel.DOFade(0, 1f).OnComplete(()=>_darkeningPanel.enabled = false);
        }
    }
}