using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Behaviours
{
    public class PowerUpIndicator : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public Sprite SetSprite { set => _spriteRenderer.sprite = value; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

}
