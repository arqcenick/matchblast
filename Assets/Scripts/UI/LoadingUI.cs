using DG.Tweening;
using Game.Events;
using UnityEngine;

namespace Game.UI
{

    public abstract class OpenCloseUI : MonoBehaviour
    {
        [SerializeField] private Transform _closed;

        [SerializeField] private bool _isClosed;

        [SerializeField] private Transform _open;

        [SerializeField] private Transform _panel;
        
        protected void Hide()
        {
            if (!_isClosed)
            {
                Move(_closed);
                _isClosed = true;
            }
        }
        
        protected void Show()
        {
            if (_isClosed)
            {
                Move(_open);
                _isClosed = false;
            }
        }
        
        private bool Move(Transform target)
        {
            _panel.transform.DOMoveY(target.position.y, 0.5f);
            return !_isClosed;
        }
    }
    

    public class LoadingUI : OpenCloseUI
    {
        
        private void Awake()
        {
            UIEvent<WillSceneChangeEvent>.Instance.AddListener(Hide);
            UIEvent<SceneReadyEvent>.Instance.AddListener(Show);
        }

        private void OnDestroy()
        {
            UIEvent<WillSceneChangeEvent>.Instance.RemoveListener(Hide);
            UIEvent<SceneReadyEvent>.Instance.AddListener(Show);
        }
        

    }
}