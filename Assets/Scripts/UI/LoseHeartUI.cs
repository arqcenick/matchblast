using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class LoseHeartUI : MonoBehaviour
    {
        private Image _heartImage;

        public void SetVisible(bool visible)
        {
            _heartImage.enabled = visible;
        }

        private void Awake()
        {
            _heartImage = GetComponent<Image>();
        }
    }
}