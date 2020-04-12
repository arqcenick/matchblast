using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Behaviours
{
    
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private List<Scene> _levels;

        private int _currentLevel;
        private int _currentHealth;
        private int _currentStars;

        private DateTime _currentDate;
        private DateTime _lastDate;

        [SerializeField] private int _healthRegenIntervalSeconds = 3600;
        private TimeSpan _requiredSpan;

        public void StartLevel()
        {
            
        }

        private void Awake()
        {
            _requiredSpan = TimeSpan.FromSeconds(_healthRegenIntervalSeconds);
            try
            {
                PlayerPrefs.GetInt("Level", _currentLevel);
                PlayerPrefs.GetInt("Stars", _currentStars);
            }
            catch
            {
                _currentLevel = 1;
                _currentStars = 0;
                _currentHealth = 5;
                PlayerPrefs.SetInt("Level", _currentLevel);
                PlayerPrefs.SetInt("Stars", _currentStars);
                PlayerPrefs.SetInt("Health", _currentHealth);
            }
     
        }
        
        private void Start()
        {
            _currentDate = DateTime.Now;
            try
            {
                long oldDate = Convert.ToInt64(PlayerPrefs.GetString("date_time"));
                _lastDate = DateTime.FromBinary(oldDate);
            }
            catch (Exception e)
            {
                _lastDate = DateTime.Now;
            }

        }

        private void Update()
        {
            TimeSpan span = DateTime.Now - _lastDate;
            int healthRegen = (int) Math.Floor(span.TotalMilliseconds / _requiredSpan.TotalMilliseconds);
            if (healthRegen >= 1)
            {
                AddHealth(healthRegen);
                _lastDate = DateTime.Now;

            }

        }

        private void AddHealth(int health)
        {
            _currentHealth = Math.Min(5, _currentHealth + health);
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetString("date_time", DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetInt("Level", _currentLevel);
            PlayerPrefs.SetInt("Stars", _currentStars);
            PlayerPrefs.SetInt("Health", _currentHealth);
        }
    }
    
    

}

