using Game.Events;
using UnityEngine;

namespace Game.UI
{
    public class NoLifeUI : MonoBehaviour
    {
        public void OnMainMenu()
        {
            UIEvent<MainMenuEvent>.Instance.Invoke();
        }
    }
}