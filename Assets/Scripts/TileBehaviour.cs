using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;
using DG.Tweening.Core.Easing;

namespace Game.Behaviours
{
    public enum MatchType
    {
        None,
        Undetermined,
        Empty,
        Bomb,
        Dynamite,
        TNT,
    }

    public enum TileColor
    {
        Blue,
        Green,
        Purple,
        Yellow,
        Red,
        None
    }
    
    public enum PowerUpType
    {
        Bomb,
        Dynamite,
        TNT,
        None,

    }
    
    
    [RequireComponent(typeof(ExplosionIndicatorBehaviour))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class TileBehaviour : MonoBehaviour
    {

        public Action<TileBehaviour> onClick;

        public TileColor ColorIndex
        {
            get => _colorIndex;
            set
            {
                _colorIndex = value;
                _spriteRenderer.sprite = _tileSprites[(int) _colorIndex];

            }
        }

        public MatchType MatchType
        {
            get => _matchType;
            set
            {
                _matchType = value;
                _explosionIndicator.ChangeExplosionType(_matchType);
            } 
        }

        public bool Destroyed => _destroyed;

        public PowerUpType PowerUp => _powerUp;

        public Vector2Int Coordinate => _coordinate;

        public Vector2 Size => _size;

        [SerializeField]
        private TileColor _colorIndex;
    
        [SerializeField]
        private Vector2 _size;
        private Vector2Int _coordinate;

        [SerializeField]
        private ExplosionIndicatorBehaviour _explosionIndicator;
        
        [SerializeField]
        private List<Sprite> _tileSprites;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private PowerUpType _powerUp = PowerUpType.None;
        private MatchType _matchType = MatchType.None;
        private bool _destroyed;
    
        
        public void DestroyTile()
        {
            _destroyed = true;
            StartCoroutine(DeathAnimation());
        }

        private IEnumerator DeathAnimation()
        {
            yield return null;
            if (_destroyed)
            {
                transform.DOScale(Vector3.one * 0.1f, 0.2f).OnComplete(()=>                Destroy(gameObject));
            }
        }
    
    
        private void OnMouseDown()
        {
            onClick(this);
        }
    
    
        public void SetPosition(Vector2 position)
        {
            // transform.DOKill();
            DOTween.defaultEaseType = Ease.InQuad;
            float duration = Mathf.Sqrt(transform.position.y - position.y) * 0.5f;
            transform.DOMove(position, duration);
        }

        public void SetCoordinate(Vector2Int vector2Int)
        {
            _coordinate = vector2Int;
        }

        public void SetAsPowerUp(PowerUpType powerUp)
        {
            _powerUp = powerUp;
            _colorIndex = TileColor.None;
            _matchType = MatchType.Empty;
            _destroyed = false;
            _spriteRenderer.sprite = PrefabAccessor.Instance.PowerUpSprites[(int) powerUp];
        }

        public void ClearPowerUp()
        {
            _powerUp = PowerUpType.None;
        }
    }
}
