using DG.Tweening;
using Game.Events;
using UnityEngine;

namespace Game.UI
{
    public class LoadingUI : MonoBehaviour
    {
        [SerializeField] private Transform _closed;

        [SerializeField] private bool _isClosed;

        [SerializeField] private Transform _open;

        [SerializeField] private Transform _panel;

        private Vector3 _previousPosition;

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

        private void Hide()
        {
            if (!_isClosed)
            {
                Move(_closed);
                _isClosed = true;
            }
        }

        private void Show()
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
}