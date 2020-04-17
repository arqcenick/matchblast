using UnityEngine;

namespace Game.Util
{
    public static class PlayerServices
    {
        private static IPlayer _player;

        private static bool _initialized;
    
        public static void Initialize(IPlayer player)
        {
            _player = player;
            _initialized = true;
        }

        public static int GetPlayerHealth()
        {
            Debug.Assert(_initialized);
            return _player.GetHealth();
        }

        public static void SetPlayerHealth(int health)
        {
            Debug.Assert(_initialized);
            _player.SetHealth(health);
        }

        public static int GetPlayerStars()
        {
            Debug.Assert(_initialized);
            return _player.GetStars();
        }

        public static int GetCurrentLevel()
        {
            return _player.GetCurrentLevel();
        }
    }
}