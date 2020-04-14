using System;
using UnityEngine.Events;

namespace Game.Events
{
    [Serializable]
    public class WillSceneChangeEvent : UnityEvent
    {
        public static WillSceneChangeEvent Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WillSceneChangeEvent();
                }

                return _instance;
            }
        }

        private static WillSceneChangeEvent _instance;
    }
}