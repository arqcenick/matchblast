using UnityEngine.Events;

namespace Game.Events
{
    public class SceneReadyEvent : UnityEvent
    {
        public static SceneReadyEvent Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneReadyEvent();
                }

                return _instance;
            }
        }
        private static SceneReadyEvent _instance;

    }
}