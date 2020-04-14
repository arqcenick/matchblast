using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Data;
using Game.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Behaviours
{
    public class LevelManager : MonoBehaviour
    {
        
        [SerializeField]
        private List<BoardSettings> _levels;

        private int _currentLevel;
        private int _currentHealth;
        private int _currentStars;

        private DateTime _currentDate;
        private DateTime _lastDate;

        [SerializeField] private int _healthRegenIntervalSeconds = 3600;
        private TimeSpan _requiredSpan;

        public void StartLevel()
        {
            WillSceneChangeEvent.Instance.Invoke();
            transform.DOMove(transform.position, 0.5f).OnComplete(() => { SceneManager.LoadScene(1);});
        }

        public void NextLevel()
        {
            _currentLevel++;
            StartLevel();
        }
        

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
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
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 1)
            {
                FindObjectOfType<BoardPersistenceBehaviour>().SetBoardSetting(_levels[_currentLevel]);
                SceneReadyEvent.Instance.Invoke();
            }
        }
        
        private void OnApplicationQuit()
        {
            PlayerPrefs.SetString("date_time", DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetInt("Level", _currentLevel);
            PlayerPrefs.SetInt("Stars", _currentStars);
            PlayerPrefs.SetInt("Health", _currentHealth);
        }
        
        private void AddHealth(int health)
        {
            //Play health gain animation
            _currentHealth = Math.Min(5, _currentHealth + health);
        }

    }
    
    

}

