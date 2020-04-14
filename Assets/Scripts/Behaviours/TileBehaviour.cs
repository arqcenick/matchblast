using System;
using System.Collections;
using DG.Tweening;
using Game.Util;
using UnityEngine;

namespace Game.Behaviours
{
    public enum MatchType
    {
        None,
        Undetermined,
        Empty,
        Bomb,
        Dynamite,
        TNT
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
        None
    }

    [RequireComponent(typeof(ExplosionIndicatorBehaviour))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class TileBehaviour : MonoBehaviour
    {
        [SerializeField] private TileColor _colorIndex;

        [SerializeField] private ExplosionIndicatorBehaviour _explosionIndicator;

        private MatchType _matchType = MatchType.None;

        [SerializeField] private Vector2 _size;


        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Action<TileBehaviour> onClick;

        public TileColor ColorIndex
        {
            get => _colorIndex;
            set
            {
                _colorIndex = value;
                _spriteRenderer.sprite = PrefabAccessor.Instance.TileSprites[(int) _colorIndex];
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

        public bool Destroyed { get; private set; }

        public PowerUpType PowerUp { get; private set; } = PowerUpType.None;

        public Vector2Int Coordinate { get; private set; }

        public Vector2 Size => _size;


        public void DestroyTile()
        {
            Destroyed = true;
            StartCoroutine(DeathAnimation());
        }

        private IEnumerator DeathAnimation()
        {
            yield return null;
            if (Destroyed) transform.DOScale(Vector3.one * 0.1f, 0.2f).OnComplete(() => SimpleObjectPool.Destroy(this));
        }


        private void OnMouseDown()
        {
            onClick(this);
        }


        public void SetPosition(Vector2 position)
        {
            // transform.DOKill();
            DOTween.defaultEaseType = Ease.InQuad;
            var duration = Mathf.Sqrt(transform.position.y - position.y) * 0.5f;
            transform.DOMove(position, duration);
        }

        public void SetCoordinate(Vector2Int vector2Int)
        {
            Coordinate = vector2Int;
        }

        public void SetAsPowerUp(PowerUpType powerUp)
        {
            PowerUp = powerUp;
            _colorIndex = TileColor.None;
            _matchType = MatchType.Empty;
            Destroyed = false;
            _spriteRenderer.sprite = PrefabAccessor.Instance.PowerUpSprites[(int) powerUp];
        }

        public void ClearPowerUp()
        {
            PowerUp = PowerUpType.None;
        }
    }
}