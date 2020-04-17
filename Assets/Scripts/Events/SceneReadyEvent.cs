using UnityEngine.Events;

namespace Game.Events
{
    public class MainMenuEvent : UnityEvent
    {
    }

    public class RetryLevelEvent : UnityEvent
    {
    }

    public class NextLevelEvent : UnityEvent
    {
    }
    
    public class OutOfLivesEvent : UnityEvent
    {
    }
    
    public class HideOutOfLivesEvent : UnityEvent
    {
    }

    public class UIEvent<T> where T : UnityEvent, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = new T();

                return _instance;
            }
        }
    }

    public class SceneReadyEvent : UnityEvent
    {
    }
}