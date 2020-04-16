using Game.Events;
using UnityEngine;

namespace Game.Behaviours
{
    public class MockLevelManager : MonoBehaviour, IPlayer
    {
        
        private void Awake()
        {
            if (FindObjectOfType<LevelManager>() == null)
            {
                PlayerServices.Initialize(this);
                UIEvent<SceneReadyEvent>.Instance.Invoke();
            }
        }

        public int GetHealth()
        {
            return 1;
        }

        public void SetHealth(int health)
        {
            
        }

        public int GetStars()
        {
            return 1;
        }

        public int GetCurrentLevel()
        {
            return 1;
        }
    }
}