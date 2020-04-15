using System.Collections.Generic;
using UnityEngine;

namespace Game.Behaviours
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ExplosionIndicatorBehaviour : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _explosions;

        private SpriteRenderer _spriteRenderer;

        public void ChangeExplosionType(MatchType type)
        {
            switch (type)
            {
                case MatchType.Bomb:
                    _spriteRenderer.sprite = _explosions[0];
                    break;
                case MatchType.Dynamite:
                    _spriteRenderer.sprite = _explosions[1];
                    break;
                case MatchType.TNT:
                    _spriteRenderer.sprite = _explosions[2];
                    break;
                default:
                    _spriteRenderer.sprite = null;
                    break;
            }
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}