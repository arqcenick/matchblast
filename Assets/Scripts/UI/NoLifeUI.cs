using System;
using DG.Tweening;
using Game.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class NoLifeUI : MonoBehaviour
    {
        private float _originalX;
        public void OnMainMenu()
        {
            UIEvent<MainMenuEvent>.Instance.Invoke();
        }

        private void Awake()
        {
            _originalX = transform.position.x;
            UIEvent<OutOfLivesEvent>.Instance.AddListener(ShowPanel);
            UIEvent<HideOutOfLivesEvent>.Instance.AddListener(HidePanel);

        }

        private void ShowPanel()
        {
            transform.transform.DOMoveX(0, 1f);
        }
        
        private void HidePanel()
        {
            transform.transform.DOMoveX(_originalX, 1f);
        }
    }
}