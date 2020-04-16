using System;
using System.Collections.Generic;
using System.Linq;
using Game.Util;
using UnityEngine;

namespace Game.Behaviours
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ExplosionIndicatorBehaviour : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public void ChangeExplosionType(MatchType type)
        {
            _spriteRenderer.sprite =
                PrefabAccessor.Instance.PowerUpDatas.FirstOrDefault(x => x.PowerUpType == type)?.MatchIcon ?? null ;
            
        }
        

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}