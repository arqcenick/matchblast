using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class WinStarUI : MonoBehaviour
    {
        public List<Transform> FlyPath => _flyPath;

        private List<Transform> _flyPath;

        private Image _starImage;

        private void Awake()
        {
            _starImage = GetComponent<Image>();
            _starImage.enabled = false;
            _flyPath = transform.GetComponentsInChildren<Transform>().Where(x=>x!=transform).ToList();
        }

        public void SetVisible(bool visible)
        {
            _starImage.enabled = visible;
        }
        
    }
}