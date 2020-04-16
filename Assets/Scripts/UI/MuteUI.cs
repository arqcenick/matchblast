using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

namespace Game.UI
{
    public class MuteUI : MonoBehaviour
    {

        private bool _isMute;
        private Image _spriteRenderer;

        public void OnMutePressed()
        {
            _isMute = !_isMute;
            SoundManager.Instance.Mute(_isMute);
            SetColor();
        }

        private void SetColor()
        {
            if (_isMute)
            {
                _spriteRenderer.color = Color.white;
            }
            else
            {
                _spriteRenderer.color = new Color(1,1,1,0.3f);
            }
        }
    
        private void Awake()
        {
            _isMute = false;
            _spriteRenderer = GetComponent<Image>();
            SetColor();
        }



}
}
