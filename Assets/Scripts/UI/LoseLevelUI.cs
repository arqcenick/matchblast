using System.Collections.Generic;
using DG.Tweening;
using Game.Behaviours;
using Game.Events;
using UnityEngine;

namespace Game.UI
{
    public class LoseLevelUI : MonoBehaviour
    {

        [SerializeField]
        private List<LoseHeartUI> _hearts;

        private int _currentHealthCount;

        [SerializeField]
        private float _heartAnimationTime;

        public void OnRetryLevel()
        {
            UIEvent<RetryLevelEvent>.Instance.Invoke();
        }

        public void OnMainMenu()
        {
            UIEvent<MainMenuEvent>.Instance.Invoke();
        }
        
        public void SetHealthVisibilities()
        {
            for (int i = 0; i < 5; i++)
            {
                _hearts[i].SetVisible(PlayerServices.GetPlayerHealth() > i);
            }
        }
        
        public void LoseHealth()
        {
            Debug.Log(PlayerServices.GetPlayerHealth());
            var currentHeart = _hearts[PlayerServices.GetPlayerHealth()];

            
            currentHeart.SetVisible(true);
            DOTween.defaultEaseType = Ease.InQuad;
            currentHeart.transform.DOMove(Vector3.down * 10, _heartAnimationTime, false);
            currentHeart.transform.DORotate(Vector3.forward * 1800, _heartAnimationTime, RotateMode.FastBeyond360);
        }
    }
}