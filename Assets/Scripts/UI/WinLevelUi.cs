using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game.Events;
using UnityEngine;

namespace Game.UI
{
    
    public class WinLevelUi : MonoBehaviour
    {
        private int _currentStarCount;

        [SerializeField] private float _starAnimationTime;

        [SerializeField] private List<WinStarUI> _stars;

        public void AddStar()
        {
            var currentStar = _stars[_currentStarCount];
            currentStar.SetVisible(true);
            var path = currentStar.FlyPath.Select(x => x.position).ToArray();
            currentStar.transform.position = path[0];
            currentStar.transform
                .DOPath(path, _starAnimationTime, PathType.CatmullRom, PathMode.Ignore);
            currentStar.transform.DOPunchScale(Vector3.one * 1.5f, _starAnimationTime, 1);
            _currentStarCount++;
        }

        public void OnNextLevel()
        {
            UIEvent<NextLevelEvent>.Instance.Invoke();
        }
    }
}