using Game.Behaviours;
using Game.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GoalCounterElementUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        private Image _spriteRenderer;
        
        
        
        public void SetTileColor(TileColor color)
        {
            _spriteRenderer.sprite = PrefabAccessor.Instance.TileSprites[(int) color];
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