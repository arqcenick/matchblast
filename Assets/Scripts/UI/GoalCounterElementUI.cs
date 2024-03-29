﻿using Game.Behaviours;
using Game.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GoalCounterElementUI : MonoBehaviour
    {
        private Image _spriteRenderer;

        [SerializeField] private TextMeshProUGUI _text;


        public void SetTileColor(TileColor color)
        {
            _spriteRenderer.color = PrefabAccessor.Instance.Colors[(int) color];
        }

        public void SetTileCount(int count)
        {
            _text.SetText(count.ToString());
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}