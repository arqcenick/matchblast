using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class WinStarUI : MonoBehaviour
    {
        private Image _starImage;
        public List<Transform> FlyPath { get; private set; }

        private void Awake()
        {
            _starImage = GetComponent<Image>();
            _starImage.enabled = false;
            FlyPath = transform.GetComponentsInChildren<Transform>().Where(x => x != transform).ToList();
        }

        public void SetVisible(bool visible)
        {
            _starImage.enabled = visible;
        }
    }
}